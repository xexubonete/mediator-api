using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    /// <summary>
    /// Query to retrieve all products
    /// </summary>
    public class GetAllProductsQuery : IRequest<IList<Product>>
    {
        /// <summary>
        /// Handler for retrieving all products
        /// </summary>
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
        {
            private readonly IApiDbContext _context;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="context">Database context</param>
            public GetAllProductsQueryHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the retrieval of all products
            /// </summary>
            /// <param name="request">Query request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>List of all products</returns>
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
