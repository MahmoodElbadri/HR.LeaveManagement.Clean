using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator:AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _requestRepository;
    private readonly ILeaveTypeRepository _typeRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository)
    {
        _requestRepository = leaveRequestRepository;
        _typeRepository = leaveTypeRepository;
        Include(new BaseLeaveRequestValidator(_requestRepository));
        RuleFor(tmp=>tmp.Id)
            .MustAsync(LeaveRequestMustExist).WithMessage("Leave Request does not exist.");
    }

    private async Task<bool> LeaveRequestMustExist(int ID, CancellationToken arg2)
    {
        var leaveRequest = await _requestRepository.GetByIdAsync(ID);
        return leaveRequest is not null;
    }
}