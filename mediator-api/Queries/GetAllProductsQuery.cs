using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    public class GetAllProductsQuery : IRequest<IList<Product>>
    {
        public class GetAllProductsQueryHandler(IApiDbContext context) : IRequestHandler<GetAllProductsQuery, IList<Product>>
        {
            private readonly IApiDbContext _context = context;

            public async Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var products = await _context.Products
                        .OrderBy(x => x.Name)
                        .ToListAsync();

                    return products;
                }
                catch (Exception ex)
                {
                    throw new Exception("", ex);
                }
            }
        }
    }
}
