using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) =>
        _leaveTypeRepository = leaveTypeRepository;

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //retrieve domain entity object
        var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(id: request.ID);
        //confirm it exists
        if (leaveTypeToDelete is null)
        {
            throw new NotFoundException(nameof(Domain.LeaveType), request.ID);
        }

        //delete it
        await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
        //just get the job done
        return Unit.Value; //it's basically saying "the operation is complete" without returning any meaningful data.
    }
}