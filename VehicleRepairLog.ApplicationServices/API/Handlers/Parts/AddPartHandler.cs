﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands;
using VehicleRepairLog.DataAccess.Entities;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Parts
{
    public class AddPartHandler : IRequestHandler<AddPartRequest, AddPartResponse>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ICommandExecutor commandExecutor;
        public AddPartHandler(IMediator mediator, ICommandExecutor commandExecutor, IMapper mapper)
        {
            this.mediator = mediator;
            this.commandExecutor = commandExecutor;
            this.mapper = mapper;
        }

        public async Task<AddPartResponse> Handle(AddPartRequest request, CancellationToken cancellationToken)
        {
            var part = this.mapper.Map<Part>(request);
            var command = new AddPartCommand()
            {
                Parameter = part
            };

            var commandFromDb = await this.commandExecutor.Execute(command);

            var response = new AddPartResponse()
            {
                Data = this.mapper.Map<Domain.Models.Part>(commandFromDb)
            };

            return response;
        }
    }
}
