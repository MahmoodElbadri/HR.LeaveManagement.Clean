using HR.LeaveManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain;

public class LeaveRequest : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int LeaveTypeID { get; set; }

    [ForeignKey("LeaveTypeID")] //this line is optional
    public LeaveType? LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestingEmployeeID { get; set; } = string.Empty;
}
