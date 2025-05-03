namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class LeaveTypeDto
{
    public int  ID { get; set; }
    public string  Name { get; set; }=string.Empty;
    public int DefaultDays { get; set; }
}