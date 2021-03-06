using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VehicleRepairLog.Application.Models;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(userDto => userDto.Id, y => y.MapFrom(user => user.Id))
                .ForMember(userDto => userDto.FirstName, y => y.MapFrom(user => user.FirstName))
                .ForMember(userDto => userDto.LastName, y => y.MapFrom(user => user.LastName))
                .ForMember(userDto => userDto.Username, y => y.MapFrom(user => user.Username))
                .ForMember(userDto => userDto.DateOfBirth, y => y.MapFrom(user => user.DateOfBirth))
                .ForMember(userDto => userDto.Email, y => y.MapFrom(user => user.Email))
                .ForMember(userDto => userDto.VehiclesBrandName, 
                                        y => y.MapFrom(user => user.Vehicles != null ?
                                            user.Vehicles.Select(vehicle => vehicle.BrandName) : new List<string>()));

            CreateMap<RegisterUserCommand, User>()
                .ForMember(user => user.Username, y => y.MapFrom(registerUserCommand => registerUserCommand.Username))
                .ForMember(user => user.Email, y => y.MapFrom(registerUserCommand => registerUserCommand.Email))
                .ForMember(user => user.Password, y => y.MapFrom(registerUserCommand => registerUserCommand.Password));
        }
    }
}
