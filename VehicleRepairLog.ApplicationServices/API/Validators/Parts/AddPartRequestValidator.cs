﻿using FluentValidation;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Validators.Parts
{
    public class AddPartRequestValidator : AbstractValidator<AddPartRequest>
    {
        public AddPartRequestValidator()
        {
            RuleFor(part => part.Name).Length(1, 100).NotNull().NotEmpty();
            RuleFor(part => part.BrandName).Length(1, 100).NotNull().NotEmpty();
            RuleFor(part => part.Price).NotEmpty();
        }
    }
}