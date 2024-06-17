using MediatR;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string? Name { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        public double Price { get; set; }

        public class CreateProductCommandHandler(IApiDbContext context) : IRequestHandler<CreateProductCommand, Product> 
        {
            private readonly IApiDbContext _context = context;

            /// <summary>Handles a request</summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Response from the request</returns>
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
