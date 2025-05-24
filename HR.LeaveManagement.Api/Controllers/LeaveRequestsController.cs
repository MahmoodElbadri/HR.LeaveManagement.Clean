using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestRepository;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.LeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.LeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly Mediator _mediator;

    public LeaveRequestsController(Mediator mediator)
    {
        this._mediator = mediator;
    }
    // GET: api/<LeaveRequestsController>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(List<LeaveRequestListDto>))]
    public async Task<ActionResult<IEnumerable<List<LeaveRequestListDto>>>> Get()
    {
        var leaveReuests = await _mediator.Send(new GetLeaveRequestListQuery());
        return Ok(leaveReuests);
    }

    // GET api/<LeaveRequestsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(LeaveRequestDetailsDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
    {
        var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailQuery { ID = id });
        return Ok(leaveRequest);
    }

    // POST api/<LeaveRequestsController>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequestCommand)
    {
        var response = await _mediator.Send(leaveRequestCommand);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT api/<LeaveRequestsController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequestCommand)
    {
        var response = await _mediator.Send(leaveRequestCommand);
        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/CancelRequest
    [HttpPut("CancelRequest")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(CancelLeaveRequestCommand cancelleaveRequestCommand)
    {
        await _mediator.Send(cancelleaveRequestCommand);
        return NoContent();
    }

    // PUT api/<LeaveRequestsController>/UpdateApproval
    [HttpPut("UpdateApproval")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Put(ChangeLeaveRequestCommand changeLeaveRequest)
    {
        await _mediator.Send(changeLeaveRequest);
        return NoContent();
    }

    // DELETE api/<LeaveRequestsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });
        return NoContent();
    }
}
