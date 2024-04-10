using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Commands
{
    public class DeleteProductCommand : IRequest
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; }
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IApiDbContext _context;

            public DeleteProductCommandHandler(IApiDbContext context)
            {
                _context = context;
            }

            public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = await _context.Products
                        .Where(x => x.Id == request.Id)
                        .FirstOrDefaultAsync();

                    if (product == null)
                    {
                        throw new Exception("No product founded" + nameof(Product));
                    }

                    _context.Products.Remove(product);

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("", ex);
                }
            }
        }
    }
}
