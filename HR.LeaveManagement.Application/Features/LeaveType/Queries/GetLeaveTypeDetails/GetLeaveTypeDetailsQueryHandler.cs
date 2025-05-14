using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
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
        if (leaveTypeDetails is null)
        {
            throw new NotFoundException("Can't be found", nameof(request));
        }

        //Convert a Data object into Dto
        var leaveTypeDetailsDto = _mapper.Map<LeaveTypeDetailsDto>(leaveTypeDetails);
        //return Data
        return leaveTypeDetailsDto;
    }
}