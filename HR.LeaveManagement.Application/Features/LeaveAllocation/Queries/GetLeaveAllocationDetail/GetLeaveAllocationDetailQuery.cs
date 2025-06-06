using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetail;

public class GetLeaveAllocationDetailQuery:IRequest<LeaveAllocationDetailDto>
{
    public int  ID { get; set; }
}