using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetail;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo, ILeaveTypeRepository leaveTypeRepo,
        IAppLogger<CreateLeaveAllocationCommandHandler> logger, IMapper mapper)
    {
        this._leaveAllocationRepo = leaveAllocationRepo;
        this._leaveTypeRepo = leaveTypeRepo;
        this._logger = logger;
        this._mapper = mapper;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveAllocationRepo);
        var validationResult = await validator.ValidateAsync(request);
        bool isLeaveTypeExist = validationResult.IsValid;
        if (validationResult.Errors.Any() && isLeaveTypeExist)
        {
            _logger.LogWarning($"There are some validation errors {0} - {1}", nameof(LeaveAllocation), request.LeaveTypeID);
            throw new BadRequestException("There are some validation errors", validationResult);
        }
        var allocations = new List<Domain.LeaveAllocation>();
        var leaveAllocationModel = _mapper.Map<Domain.LeaveAllocation>(request);
        allocations.Add(leaveAllocationModel);
        return Unit.Value;
    }
}
