using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using MediatR.Pipeline;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.LeaveRequestDetails;

public class GetLeaveRequestQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repo;

    public GetLeaveRequestQueryHandler(IMapper mapper, ILeaveRequestRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request,
        CancellationToken cancellationToken)
    {
        var leaveRequest = await _repo.GetLeaveRequestWithDetails(request.ID);
        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.ID);
        }

        return _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
    }
}