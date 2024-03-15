using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    public class GetAllProductsQuery : IRequest<IList<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
        {
            private readonly IApiDbContext _context;

            public GetAllProductsQueryHandler(IApiDbContext context)
            {
                _context = context;
            }

            public async Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var products = await _context.Products
                    .OrderBy(x => x.Name)
                    .ToListAsync();

                return products;
            }
        }
    }
}
