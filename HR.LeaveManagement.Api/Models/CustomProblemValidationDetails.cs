using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Models;

public class CustomProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors = new Dictionary<string, string[]>();
}
