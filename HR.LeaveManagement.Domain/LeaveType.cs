namespace HR.LeaveManagement.Domain;

public class LeaveType
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}
