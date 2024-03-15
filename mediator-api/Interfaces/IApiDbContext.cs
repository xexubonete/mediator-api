using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;

namespace webapi_docker.Interfaces
{
    public interface IApiDbContext 
    {
        DbSet<Product> Products { get; set; }

        public Task<int> SaveChangesAsync();
            
    }
}
