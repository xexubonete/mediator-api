using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;

namespace webapi_docker.Interfaces
{
    /// <summary>
    /// Interface for the API database context
    /// </summary>
    public interface IApiDbContext 
    {
        /// <summary>
        /// Gets or sets the products collection
        /// </summary>
        DbSet<Product> Products { get; set; }

        /// <summary>
        /// Saves changes to the database asynchronously
        /// </summary>
        /// <returns>The number of modified entities</returns>
        public Task<int> SaveChangesAsync();
    }
}
