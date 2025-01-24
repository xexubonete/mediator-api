using MediatR;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Commands
{
    /// <summary>
    /// Command to update an existing product
    /// </summary>
    public class UpdateProductCommand : IRequest<Product>
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the product price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Handler for updating a product
        /// </summary>
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
        {
            private readonly IApiDbContext _context;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="context">Database context</param>
            public UpdateProductCommandHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the product update
            /// </summary>
            /// <param name="request">Update command</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>The updated product</returns>
            public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await _context.Products
                        .FindAsync(request.Id);

                    if (product == null)
                    {
                        throw new Exception("No product founded" + nameof(Product));
                    }

                    product.Name = request.Name;
                    product.Price = request.Price;

                    await _context.SaveChangesAsync();

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
