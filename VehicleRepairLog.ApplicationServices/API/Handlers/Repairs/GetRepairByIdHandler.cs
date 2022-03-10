﻿using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.Domain.Requests.Repairs;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Repairs;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;
using VehicleRepairLog.DataAccess;
using VehicleRepairLog.DataAccess.CQRS.Queries.Repairs;

namespace VehicleRepairLog.ApplicationServices.API.Handlers.Repairs
{
    public class GetRepairByIdHandler : IRequestHandler<GetRepairByIdRequest, GetRepairByIdResponse>
    {
        private readonly IMapper mapper;
        private readonly IQueryExecutor queryExecutor;

        public GetRepairByIdHandler(IMapper mapper, IQueryExecutor queryExecutor)
        {
            this.mapper = mapper;
            this.queryExecutor = queryExecutor;
        }

        public async Task<GetRepairByIdResponse> Handle(GetRepairByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetRepairByIdQuery()
            {
                Id = request.RepairId
            };
            var repair = await this.queryExecutor.Execute(query);

            if (repair is null)
            {
                return new GetRepairByIdResponse()
                {
                    Error = new ErrorModel(ErrorType.NotFound)
                };
            }

            return new GetRepairByIdResponse()
            {
                Data = this.mapper.Map<Domain.Models.RepairDto>(repair)
            };
        }
    }
}
