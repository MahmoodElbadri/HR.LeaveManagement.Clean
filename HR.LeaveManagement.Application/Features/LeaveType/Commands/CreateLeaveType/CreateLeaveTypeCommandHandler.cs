using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler:IRequestHandler<CreateLeaveTypeCommand,int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //validate incoming data
        
        //convert to a domain entity object
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);
        //save to db
        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);
        //return record id
        return leaveTypeToCreate.ID;
    }
}