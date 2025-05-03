using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler:IRequestHandler<UpdateLeaveTypeCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate the object
        
        //convert dto to object model
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);
        //save to db
        await  _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
        //just get the job done
        return Unit.Value;
    }
}