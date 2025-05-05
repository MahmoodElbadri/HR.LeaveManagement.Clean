using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Include(tmp => tmp.LeaveType)
            .FirstOrDefaultAsync(tmp => tmp.ID == id);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(tmp => tmp.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
    {
        return await _context.LeaveAllocations
            .Include(tmp => tmp.LeaveType)
            .Where(tmp => tmp.EmployeeID == userId)
            .ToListAsync();
    }

    public async Task<bool> AllocationExist(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(tmp => tmp.EmployeeID == userId
                                                               && tmp.Period == period
                                                               && tmp.LeaveTypeID == leaveTypeId);
    }

    public async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
    {
        await _context.AddRangeAsync(leaveAllocations);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .Include(tmp => tmp.LeaveType)
            .FirstOrDefaultAsync(tmp => tmp.EmployeeID == userId
                                        &&
                                        tmp.LeaveTypeID == leaveTypeId);
    }
}