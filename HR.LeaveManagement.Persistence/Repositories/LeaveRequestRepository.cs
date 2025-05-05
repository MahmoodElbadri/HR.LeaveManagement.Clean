using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await _context
            .LeaveRequests
            .AsNoTracking()
            .Include(tmp=>tmp.LeaveType)
            .FirstOrDefaultAsync(tmp => tmp.ID == id);
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await _context
            .LeaveRequests
            .Include(tmp => tmp.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await _context
            .LeaveRequests
            .Where(tmp=>tmp.RequestingEmployeeID==userId)
            .Include(tmp=>tmp.LeaveType)
            .ToListAsync();
        return leaveRequests;
    }
}