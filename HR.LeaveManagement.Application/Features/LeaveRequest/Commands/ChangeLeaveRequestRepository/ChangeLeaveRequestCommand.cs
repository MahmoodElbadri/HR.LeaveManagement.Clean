using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestRepository;

public class ChangeLeaveRequestCommand:IRequest<Unit>
{
    public int  ID { get; set; }
    public bool Approved { get; set; }
}