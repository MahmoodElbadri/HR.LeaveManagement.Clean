using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestRepository;

public class ChangeLeaveRequestCommandValidator:AbstractValidator<ChangeLeaveRequestCommand>
{
    public ChangeLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository)
    {
        RuleFor(tmp=>tmp.Approved)
            .NotNull()
            .WithMessage("Approval status is required.");
    } 
}