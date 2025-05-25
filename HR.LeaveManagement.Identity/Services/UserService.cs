using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        this._userManager = userManager;
    }
    public async Task<Employee> GetEmployee(string id)
    {
        var employee = await _userManager.FindByIdAsync(id);
        return new Employee
        {
            Email = employee.Email,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Id = employee.Id,
        };
    }

    public Task<List<Employee>> GetEmployees()
    {
        throw new NotImplementedException();
    }
}
