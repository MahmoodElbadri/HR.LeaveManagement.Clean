using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class LeaveAllocationDto //DTO data transfer object we tell the front end what specific data we need
{
    public int  ID { get; set; }
    public int NumberOfDays { get; set; }
    public int LeaveTypeID { get; set; }
    public LeaveTypeDto? LeaveType { get; set; } //Navigation property 
    public int Period { get; set; }
}