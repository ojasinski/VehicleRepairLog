﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses.Parts;

namespace VehicleRepairLog.ApplicationServices.API.Domain.Requests.Parts
{
    public class GetPartByIdRequest : IRequest<GetPartByIdResponse>
    {
        public int PartId { get; set; }
    }
}