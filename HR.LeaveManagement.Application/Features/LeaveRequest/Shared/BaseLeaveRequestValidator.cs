using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveRequestRepository _repo;

    public BaseLeaveRequestValidator(ILeaveRequestRepository repo)
    {
        _repo = repo;
        RuleFor(tmp => tmp.LeaveTypeId)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("Leave Type does not exist.");
        RuleFor(tmp => tmp.StartDate)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage("Start Date must be in the current time.");

        RuleFor(tmp => tmp.EndDate)
            .GreaterThanOrEqualTo(tmp => tmp.StartDate)
            .WithMessage("End Date must be after Start Date.");
    }

    private async Task<bool> LeaveTypeMustExist(int arg1, CancellationToken arg2)
    {
        var leaveType = await _repo.GetByIdAsync(arg1);
        return leaveType is not null;
    }
}