using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class LeaveAllocationDto
{
    public int  ID { get; set; }
    public int NumberOfDays { get; set; }
    public int LeaveTypeID { get; set; }
    public LeaveTypeDto? LeaveType { get; set; }
    public int Period { get; set; }
}