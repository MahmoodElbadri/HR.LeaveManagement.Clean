using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator:AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        RuleFor(tmp => tmp.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .MaximumLength(70).WithMessage("{propertyName} must be less than 70 characters.")
            .NotNull();
        
        RuleFor(tmp=>tmp.DefaultDays)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than or equal to 1.")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");

        RuleFor(tmp => tmp)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage("Leave Type Name must be unique");
    }

    private Task<bool> LeaveTypeNameUnique(CreateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}