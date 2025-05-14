using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    public IMediator _mediator { get; }

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    // GET: api/<LeaveTypesController>
    [HttpGet]
    public async Task<List<LeaveTypeDto>> Get()
    {
        var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
        return leaveTypes;
    }

    // GET api/<LeaveTypesController>/5
    [HttpGet("{id}")]
    public async Task<LeaveTypeDetailsDto> Get(int id)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
        return leaveType;
    }

    // POST api/<LeaveTypesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LeaveTypeDetailsDto leaveType)
    {
        var response = await _mediator.Send(leaveType);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveTypesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<LeaveTypesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
