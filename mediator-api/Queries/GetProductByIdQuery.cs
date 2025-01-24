using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    /// <summary>
    /// Query to retrieve a product by its identifier
    /// </summary>
    public class GetProductByIdQuery : IRequest<Product>
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Handler for retrieving a product by its identifier
        /// </summary>
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IApiDbContext _context;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="context">Database context</param>
            public GetProductByIdQueryHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the retrieval of a product by its identifier
            /// </summary>
            /// <param name="request">Query request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>The found product</returns>
            public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await _context.Products
                        .FirstOrDefaultAsync(x => x.Id == request.Id);

                    if (product == null)
                    {
                        throw new Exception("No product founded" + nameof(Product));
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
