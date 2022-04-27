﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;

namespace VehicleRepairLog.Application.Features.Repairs
{
    public class AddRepairCommand : IRequest<RepairDto>
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CarWorkshopName { get; set; }
        public int VehicleId { get; set; }
        public List<string> PartNames { get; set; }
    }

    public class AddRepairCommandHandler : IRequestHandler<AddRepairCommand, RepairDto>
    {
        private readonly IMapper mapper;
        private readonly IRepairRepository repairRepository;
        private readonly IPartRepository partRepository;

        public AddRepairCommandHandler(IMapper mapper, IRepairRepository repairRepository, IPartRepository partRepository)
        {
            this.mapper = mapper;
            this.repairRepository = repairRepository;
            this.partRepository = partRepository;
        }

        public async Task<RepairDto> Handle(AddRepairCommand request, CancellationToken cancellationToken)
        {
            var repair = this.mapper.Map<Repair>(request);

            var parts = await this.partRepository.GetByNameAsync(request.PartNames);
            repair.Parts = parts;

            await this.repairRepository.AddAsync(repair, request.PartNames);

            return this.mapper.Map<RepairDto>(repair);
        }
    }
}
