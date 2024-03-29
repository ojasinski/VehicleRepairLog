﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Application.Features.Users.ChangeUserPassword;
using VehicleRepairLog.Application.Features.Users.DeleteUser;
using VehicleRepairLog.Application.Features.Users.UpdateUser;
using VehicleRepairLog.Shared.DtoModels;

namespace VehicleRepairLog.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Endpoint for creating User Accounts.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
        {
            // Sends Command with data from HTTP Request to Handler. Receives response.
            RegisterResultDto response = await _mediator.Send(command);

            // Returns HTTP Response with status code 200(OK).
            return Ok(response);
        }

        /// <summary>
        ///     Endpoint for User Authentication.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginRequestDto loginRequest)
        {
            // Assigns data from HTTP Request to Command.
            AuthenticateUserCommand command = new()
            {
                Password = loginRequest.Password,
                Email = loginRequest.Email
            };

            // Sends Command with given data to Handler. Receives response.
            LoginResultDto response = await _mediator.Send(command);

            // Returns HTTP Response with status code 200(OK).
            return Ok(response);
        }

        /// <summary>
        ///     Endpoint for getting User data of given ID.
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            // Assigns data from HTTP Request to Command.
            GetUserByIdQuery query = new()
            {
                UserId = userId
            };

            // Sends Query with given data to Handler. Receives response.
            UserDto response = await _mediator.Send(query);

            // Returns HTTP Response with status code 200(OK).
            return Ok(response);
        }

        /// <summary>
        ///     Endpoint for updating User data.
        /// </summary>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserCommand command, int userId)
        {
            command.UserId = userId;

            // Sends Command with given data to Handler. Receives response.
            UserDto response = await _mediator.Send(command);

            // Returns HTTP Response with status code 200(OK).
            return Ok(response);
        }

        /// <summary>
        ///     Endpoint for updating User password.
        /// </summary>
        [HttpPut("PasswordChange/{userId}")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command, int userId)
        {
            command.UserId = userId;

            // Sends Command with given data to Handler.
            await _mediator.Send(command);

            // Returns HTTP Response with status code 200(OK).
            return NoContent();
        }

        /// <summary>
        ///     Endpoint for deleting User Accounts.
        /// </summary>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            // Assigns data from HTTP Request to Command.
            DeleteUserCommand command = new()
            {
                UserId = userId
            };

            // Sends Command with given data to Handler.
            await _mediator.Send(command);

            // Returns HTTP Response with status code 204(No Content).
            return NoContent();
        }
    }
}
