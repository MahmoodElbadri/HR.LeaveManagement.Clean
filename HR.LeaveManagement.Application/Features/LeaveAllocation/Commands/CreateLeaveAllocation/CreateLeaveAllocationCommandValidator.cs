using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _repo;

    public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository repo)
    {
        RuleFor(tmp => tmp.LeaveTypeID).NotEmpty()
            .MustAsync(MustExist)
            .WithMessage("Leave Allocation must exist")
            .GreaterThan(0);
        this._repo = repo;
    }

    private async Task<bool> MustExist(int id, CancellationToken token)
    {
        var leaveAllocation = await _repo.GetByIdAsync(id);
        return leaveAllocation is not null;
    }
}
