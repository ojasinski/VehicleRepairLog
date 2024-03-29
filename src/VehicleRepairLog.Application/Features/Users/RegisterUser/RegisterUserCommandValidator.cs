﻿using FluentValidation;
using System.Linq;
using VehicleRepairLog.Infrastructure;

namespace VehicleRepairLog.Application.Features.Users
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(VehicleRepairLogContext dbContext)
        {
            RuleFor(user => user.FirstName)
                .MaximumLength(50);

            RuleFor(user => user.LastName)
                .MaximumLength(50);

            RuleFor(user => user.Email)
                .EmailAddress()
                .NotEmpty();
                
            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(user => user.ConfirmPassword).Equal(user => user.Password);

            RuleFor(user => user.Username)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(user => user.Email)
                .Custom((value, context) =>
                {
                    bool emailInUse = dbContext.Users.Any(user => user.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("This Email is already in use.");
                    }
                });

            RuleFor(user => user.Username)
                .Custom((value, context) =>
                {
                    bool usernameInUse = dbContext.Users.Any(user => user.Username == value);

                    if (usernameInUse)
                    {
                        context.AddFailure("This username is taken.");
                    }
                });
        }
    }
}
