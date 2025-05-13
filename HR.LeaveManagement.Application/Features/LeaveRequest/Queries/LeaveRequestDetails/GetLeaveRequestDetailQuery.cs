using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.LeaveRequestDetails;

public class GetLeaveRequestDetailQuery:IRequest<LeaveRequestDetailsDto>
{
    public int ID { get; set; }
}