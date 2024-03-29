﻿using FluentValidation;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class AddRepairCommandValidator : AbstractValidator<AddRepairCommand>
    {
        public AddRepairCommandValidator()
        {
            RuleFor(repair => repair.CreatedDate).NotEmpty();
            RuleFor(repair => repair.Description).MaximumLength(1000);
            RuleFor(repair => repair.CarWorkshopName).MaximumLength(1000).NotEmpty();
            RuleFor(repair => repair.VehicleId).NotEmpty();
        }
    }
}
