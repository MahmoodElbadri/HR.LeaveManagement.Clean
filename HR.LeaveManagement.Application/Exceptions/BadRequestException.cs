using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; set; } = new();

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
    }
}