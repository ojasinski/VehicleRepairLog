﻿using FluentValidation;

namespace VehicleRepairLog.Application.Features.Vehicles
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator()
        {
            RuleFor(vehicle => vehicle.Name).MaximumLength(25);
            RuleFor(vehicle => vehicle.BrandName).MaximumLength(25);
            RuleFor(vehicle => vehicle.VinNumber).MaximumLength(25).NotEmpty();
            RuleFor(vehicle => vehicle.PaintColor).MaximumLength(20);
            RuleFor(vehicle => vehicle.FuelType).MaximumLength(20);
            RuleFor(vehicle => vehicle.Mileage).InclusiveBetween(0, 10000000);
        }
    }
}
