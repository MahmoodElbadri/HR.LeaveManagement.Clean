using System.ComponentModel.DataAnnotations;
using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _allocationRepository;
    private readonly ILeaveTypeRepository _typeRepository;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository
        , IMapper mapper)
    {
        _allocationRepository = leaveAllocationRepository;
        _typeRepository = leaveTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_typeRepository, _allocationRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid Leave Allocation Update", validationResult);
        }

        var leaveAllocation = await _allocationRepository
            .GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);

        var leaveType = await _typeRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);

        var leaveAllocationToUpdate = _mapper.Map<Domain.LeaveAllocation>(request);
        await _allocationRepository.UpdateAsync(leaveAllocationToUpdate);
        return Unit.Value;
    }
}