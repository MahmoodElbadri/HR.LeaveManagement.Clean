using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(tmp => tmp.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be less than 70 characters.")
            .MinimumLength(1).WithMessage("{PropertyName} must be greater than or equal to 1.");
        RuleFor(tmp => tmp.DefaultDays)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");

        RuleFor(tmp => tmp)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave Type Name must be unique.");

        RuleFor(tmp => tmp.Id)
            .NotNull()
            .MustAsync(LeaveTypeMustExist);
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType is not null;
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}