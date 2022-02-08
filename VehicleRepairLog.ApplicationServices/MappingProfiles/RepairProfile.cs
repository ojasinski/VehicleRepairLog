﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRepairLog.ApplicationServices.MappingProfiles
{
    public class RepairProfile : Profile
    {
        public RepairProfile()
        {
            CreateMap<DataAccess.Entites.Repair, API.Domain.Models.Repair>()
                .ForMember(x => x.Id, y => y.MapFrom(z => z.Id))
                .ForMember(x => x.Date, y => y.MapFrom(z => z.Date))
                .ForMember(x => x.Description, y => y.MapFrom(z => z.Description))
                .ForMember(x => x.CarWorkshopName, y => y.MapFrom(z => z.CarWorkshopName));
        }
    }
}
