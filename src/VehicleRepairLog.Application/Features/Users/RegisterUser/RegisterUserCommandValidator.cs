﻿using FluentValidation;
using System.Linq;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(VehicleProfileStorageContext dbContext)
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(100);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.FirstName).MaximumLength(100);

            RuleFor(x => x.LastName).MaximumLength(100);

            RuleFor(x => x.Username).MaximumLength(100).NotEmpty();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Try other email address.");
                    }
                });

            RuleFor(x => x.Username)
                .Custom((value, context) =>
                {
                    var usernameInUse = dbContext.Users.Any(x => x.Username == value);

                    if (usernameInUse)
                    {
                        context.AddFailure("This username is taken.");
                    }
                });
        }
    }
}