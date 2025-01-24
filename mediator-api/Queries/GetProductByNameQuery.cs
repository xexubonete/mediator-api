using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    /// <summary>
    /// Query to retrieve a product by its name
    /// </summary>
    public class GetProductByNameQuery : IRequest<Product>
    {
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Handler for retrieving a product by its name
        /// </summary>
        public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Product>
        {
            private readonly IApiDbContext _context;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="context">Database context</param>
            public GetProductByNameQueryHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the retrieval of a product by its name
            /// </summary>
            /// <param name="request">Query request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>The found product</returns>
            public async Task<Product> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(x => x.Name == request.Name);

                    if (product == null)
                    {
                        throw new Exception("No names founded" + nameof(Product));
                    }

                    return product;
                }
                catch (Exception ex)
                {
                    throw new Exception("", ex);
                }
            }
        }
    }
}
