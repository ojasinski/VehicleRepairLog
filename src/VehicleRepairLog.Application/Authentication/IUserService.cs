﻿using VehicleRepairLog.Application.Models;

namespace VehicleRepairLog.Application.Authentication
{
    public interface IUserService
    {
        UserDto GetCurrentUser();
    }
}