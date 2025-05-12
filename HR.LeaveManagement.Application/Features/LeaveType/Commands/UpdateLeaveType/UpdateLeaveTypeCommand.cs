using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class
    UpdateLeaveTypeCommand : IRequest<Unit> //unit here is like saying nothing is important it's like a placeholder to say i don't need a result just do the job
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}