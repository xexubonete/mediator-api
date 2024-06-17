using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        public class GetProductByIdQueryHandler(IApiDbContext context) : IRequestHandler<GetProductByIdQuery, Product>
        {
            private readonly IApiDbContext _context = context;

            /// <summary>Handles a request</summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Response from the request</returns>
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
