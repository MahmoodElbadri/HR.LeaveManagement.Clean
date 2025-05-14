using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator:AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _repo;
    private readonly ILeaveAllocationRepository _allocationRepo;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository repo, ILeaveAllocationRepository leaveAllocationRepo)
    {
        _repo = repo;
        _allocationRepo = leaveAllocationRepo;
        RuleFor(tmp => tmp.Id)
            .NotNull()
            .WithMessage("{PropertyName} is required.")
            .MustAsync(LeaveAllocationMustExist).WithMessage("Leave Allocation does not exist.");
        
        RuleFor(tmp=>tmp.LeaveTypeId)
            .MustAsync(LeaveTypeMustExist)
            .WithMessage("Leave Type does not exist.");
        
        RuleFor(tmp => tmp.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0.");
        
        RuleFor(tmp=>tmp.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be in the future.");
        
        
    }

    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
    {
        var leaveAllocation = await _allocationRepo.GetByIdAsync(id);
        return leaveAllocation is not null;
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveAllocation = await _repo.GetByIdAsync(id);
        return leaveAllocation is not null; 
    }
}