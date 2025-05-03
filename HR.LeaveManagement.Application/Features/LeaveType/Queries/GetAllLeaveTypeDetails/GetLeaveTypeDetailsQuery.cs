using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetails;

public record GetLeaveTypeDetailsQuery(int id):IRequest<LeaveTypeDetailsDto>;