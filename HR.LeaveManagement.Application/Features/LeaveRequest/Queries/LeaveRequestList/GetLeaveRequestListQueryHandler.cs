using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.LeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repo;

    public GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequests = await _repo.GetLeaveRequestsWithDetails();
        return _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
    }
}