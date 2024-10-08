﻿using Microsoft.AspNetCore.Mvc;
using webapi_docker.Commands;
using webapi_docker.Entities;
using webapi_docker.Queries;

namespace webapi_docker.Controllers
{
    public class ProductController : ApiController     
    {

        /// <summary>Gets all products.</summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IList<Product>>> GetAllProducts() 
        {
            var products = await this.Mediator.Send(new GetAllProductsQuery());

            if (!products.Any())
            {
                return NoContent();
            }

            return Ok(products);
        }

        /// <summary>Gets the product by identifier.</summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await this.Mediator.Send(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>Gets the name of the product by.</summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet("name")]
        public async Task<ActionResult<Product>> GetProductByName(string Name)
        {
            var product = await this.Mediator.Send(new GetProductByNameQuery { Name = Name });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>Creates the product.</summary>
        /// <param name="command">The command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Product>> CreateProduct(IList<CreateProductCommand> commands)
        {
            IList<Product> products = new List<Product>();
            foreach (var command in commands)
            {
                var product = await this.Mediator.Send(command);
                products.Add(product);
            }

            if (!products.Any())
            {
                return BadRequest();
            }

            return Ok(products);
        }

        /// <summary>Updates the product.</summary>
        /// <param name="command">The command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Guid id, UpdateProductCommand command)
        {
            var product = await this.Mediator.Send(command);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>Deletes the product.</summary>
        /// <param name="command">The command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            await this.Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok();
        }
    }
}
