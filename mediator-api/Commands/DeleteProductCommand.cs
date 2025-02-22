﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using webapi_docker.Entities;
using webapi_docker.Interfaces;

namespace webapi_docker.Commands
{
    /// <summary>
    /// Command to delete an existing product
    /// </summary>
    public class DeleteProductCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Handler for deleting a product
        /// </summary>
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IApiDbContext _context;

            /// <summary>
            /// Initializes a new instance of the handler
            /// </summary>
            /// <param name="context">Database context</param>
            public DeleteProductCommandHandler(IApiDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Handles the product deletion
            /// </summary>
            /// <param name="request">Deletion command</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>Task representing the asynchronous operation</returns>
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
