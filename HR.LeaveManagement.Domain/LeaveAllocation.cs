using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public int LeaveTypeID { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int Period { get; set; }
    public string EmployeeID { get; set; } = String.Empty;
}
