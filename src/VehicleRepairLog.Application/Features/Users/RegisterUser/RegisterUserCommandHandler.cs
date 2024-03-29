﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.Application.Features.Users
{
    public class RegisterUserCommand : IRequest<RegisterResultDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResultDto>
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly VehicleRepairLogContext _context;

        public RegisterUserCommandHandler(IMapper mapper, IPasswordHasher<User> passwordHasher, VehicleRepairLogContext context)
        {
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task<RegisterResultDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            string hashedPassword = _passwordHasher.HashPassword(user, request.Password);
            user.Password = hashedPassword;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterResultDto
            {
                Successful = true
            };
        }
    }
}
