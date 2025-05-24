using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetail;

public class GetLeaveAllocationDetailQueryHandler:IRequestHandler<GetLeaveAllocationDetailQuery,LeaveAllocationDetailDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _repo;

    public GetLeaveAllocationDetailQueryHandler(IMapper mapper, ILeaveAllocationRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
    public async Task<LeaveAllocationDetailDto> Handle(GetLeaveAllocationDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _repo.GetLeaveAllocationWithDetails(request.ID); //the id which we have from the query remember it?
        if (leaveAllocation is null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.ID);
        }
        return _mapper.Map<LeaveAllocationDetailDto>(leaveAllocation);
    }
}