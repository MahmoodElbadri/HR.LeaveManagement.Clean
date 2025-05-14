using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler:IRequestHandler<DeleteLeaveAllocationCommand,Unit>
{
    private readonly ILeaveAllocationRepository _repo;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository repo)
    {
        _repo = repo;
    }
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _repo.GetByIdAsync(request.ID);
        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.ID); 
        }

        return Unit.Value;
    }
}