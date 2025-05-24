using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IEmailSender _emailSender;
    private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;

    public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IEmailSender emailSender, IAppLogger<CancelLeaveRequestCommandHandler> logger)
    {
        this._leaveRequestRepository = leaveRequestRepository;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._emailSender = emailSender;
        this._logger = logger;
    }

    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.ID);

        if (leaveRequest is null)
            throw new NotFoundException(nameof(leaveRequest), request.ID);

        leaveRequest.Cancelled = true;
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // if already approved, re-evaluate the employee's allocations for the leave type
        if (leaveRequest.Approved == true)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeID,
                leaveRequest.LeaveTypeID);
            allocation.NumberOfDays += daysRequested;

            await _leaveAllocationRepository.UpdateAsync(allocation);
        }


        // send confirmation email
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body =
                    $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully.",
                Subject = "Leave Request Cancelled"
            };

            await _emailSender.SendEmail(email);
        }
        catch (Exception)
        {
            // log error
            _logger.LogWarning($"Error sending email to user {leaveRequest.RequestingEmployeeID} about leave request cancellation.");
        }

        return Unit.Value;
    }
}