using MediatR;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Commands
{
    /// <summary>
    /// Command to create a new product
    /// </summary>
    public class CreateProductCommand : IRequest<Product>
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
        /// Handler for the create product command
        /// </summary>
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
        {
            private readonly IApiDbContext _context;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="context">Database context</param>
            public CreateProductCommandHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the product creation
            /// </summary>
            /// <param name="request">Creation command</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>The created product</returns>
            public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var newProduct = new Product
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Name,
                        Price = request.Price
                    };

                    _context.Products.Add(newProduct);

                    await _context.SaveChangesAsync();

                    return newProduct;
                }
                catch (Exception ex)
                {
                    throw new Exception("", ex);
                }
            }
        }
    }
}
