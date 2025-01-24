using Microsoft.EntityFrameworkCore;
using System;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Persistence
{
    /// <summary>
    /// Database context for the API
    /// </summary>
    public class ApiDbContext : DbContext, IApiDbContext
    {
        /// <summary>
        /// Initializes a new instance of the API database context
        /// </summary>
        /// <param name="options">The options to be used by the context</param>
        public ApiDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Gets or sets the products collection
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Saves all changes made in this context to the database asynchronously
        /// </summary>
        /// <returns>The number of state entries written to the database</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
