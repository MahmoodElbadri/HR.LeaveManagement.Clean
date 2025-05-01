using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation
{
    public int ID { get; set; }
    public int NumberOfDays { get; set; }
    public LeaveType LeaveType { get; set; }
    public int LeaveTypeID { get; set; }
    public int Period { get; set; }
}
