using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestRepository;

public class ChangeLeaveRequestCommandHandler : IRequestHandler<ChangeLeaveRequestCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public ChangeLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepository,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IMapper mapper,
        IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        this._leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        this._emailSender = emailSender;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.ID);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.ID);

        leaveRequest.Approved = request.Approved;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // if request is approved, get and update the employee's allocations
        if (request.Approved)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation =
                await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeID,
                    leaveRequest.LeaveTypeID);
            allocation.NumberOfDays -= daysRequested;

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body =
                    $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };
            await _emailSender.SendEmail(email);
        }
        catch (Exception)
        {
            // log error
        }

        return Unit.Value;
    }
}