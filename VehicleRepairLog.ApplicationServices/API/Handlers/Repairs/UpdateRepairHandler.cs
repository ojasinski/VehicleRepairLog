﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Commands.Repairs;
using VehicleRepairLog.DataAccess.CQRS.Queries.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class UpdateRepairHandler : IRequestHandler<UpdateRepairRequest, UpdateRepairResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;
        private readonly ICommandExecutor commandExecutor;

        public UpdateRepairHandler(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
            this.commandExecutor = commandExecutor;
        }

        public async Task<UpdateRepairResponse> Handle(UpdateRepairRequest request, CancellationToken cancellationToken)
        {
            var query = new GetRepairByIdQuery() 
            {
                Id = request.RepairId
            };
            var repair = await this.queryExecutor.Execute(query);

            var command = new UpdateRepairCommand()
            {
                Parameter = this.mapper.Map(request, repair)
            };
            var updatedRepair = await this.commandExecutor.Execute(command);

            return new UpdateRepairResponse()
            {
                Data = this.mapper.Map<Domain.Models.Repair>(updatedRepair)
            };
        }
    }
}