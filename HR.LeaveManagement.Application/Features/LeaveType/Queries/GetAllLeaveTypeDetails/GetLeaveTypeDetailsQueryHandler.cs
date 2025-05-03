using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler:IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        //Query DB
        var leaveTypeDetails = await _leaveTypeRepository.GetByIdAsync(request.id);
        //Convert a Data object into Dto
        var leaveTypeDetailsDto = _mapper.Map<LeaveTypeDetailsDto>(leaveTypeDetails);
        //return Data
        return leaveTypeDetailsDto;
    }
}