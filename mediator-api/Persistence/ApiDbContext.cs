using Microsoft.EntityFrameworkCore;
using System;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Persistence
{
    public class ApiDbContext : DbContext, IApiDbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options){ }

        public DbSet<Product> Products { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
