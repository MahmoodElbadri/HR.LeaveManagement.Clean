using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetLeaveAllocationListQuery:IRequest<List<LeaveAllocationDto>>
{
 public int  ID { get; set; }   
}