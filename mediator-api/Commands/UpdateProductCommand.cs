using MediatR;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Commands
{
    public class UpdateProductCommand : IRequest<Product>
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

        public class UpdateProductCommandHandler(IApiDbContext context) : IRequestHandler<UpdateProductCommand, Product>
        {
            private readonly IApiDbContext _context = context;

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
