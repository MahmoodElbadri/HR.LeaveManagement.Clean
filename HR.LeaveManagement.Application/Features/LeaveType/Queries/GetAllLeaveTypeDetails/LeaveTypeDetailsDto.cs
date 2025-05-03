namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetails;

public class LeaveTypeDetailsDto
{
    public int  ID { get; set; }
    public string  Name { get; set; }=string.Empty;
    public int DefaultDays { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}