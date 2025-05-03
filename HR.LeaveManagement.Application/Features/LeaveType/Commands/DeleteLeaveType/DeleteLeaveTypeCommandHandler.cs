using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) =>
        _leaveTypeRepository = leaveTypeRepository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //confirm it exists
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(id: request.ID);

        //delete it
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
        //just get the job done
        return Unit.Value; //it's basically saying "the operation is complete" without returning any meaningful data.
    }
}