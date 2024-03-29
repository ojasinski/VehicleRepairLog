﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Infrastructure.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly VehicleRepairLogContext context;

        public PartRepository(VehicleRepairLogContext context)
        {
            this.context = context;
        }

        public Task<List<Part>> GetByNameAsync(List<string> partNames)
        {
            return this.context.Parts.Where(x => partNames.Contains(x.Name)).ToListAsync();
        }

        public Task AddAsync(Part part)
        {
            this.context.Parts.Add(part);
            return this.context.SaveChangesAsync();
        }

        public Task<List<Part>> GetAllAsync()
        {
            return this.context.Parts.ToListAsync();
        }

        public Task<Part> GetByIdAsync(int id)
        {
            return this.context.Parts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task RemoveAsync(Part part)
        {
            this.context.Parts.Remove(part);
            return this.context.SaveChangesAsync();
        }

        public  Task UpdateAsync(Part part)
        {
            this.context.Parts.Update(part);
            return this.context.SaveChangesAsync();
        }
    }
}
