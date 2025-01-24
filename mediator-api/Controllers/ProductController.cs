using Microsoft.AspNetCore.Mvc;
using webapi_docker.Commands;
using webapi_docker.Entities;
using webapi_docker.Queries;

namespace webapi_docker.Controllers
{
    /// <summary>
    /// Controller for managing product CRUD operations
    /// </summary>
    public class ProductController : ApiController     
    {
        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns>List of products</returns>
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

        /// <summary>
        /// Gets a product by its identifier
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <returns>The found product</returns>
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

        /// <summary>
        /// Gets a product by its name
        /// </summary>
        /// <param name="Name">Product name to search</param>
        /// <returns>The found product</returns>
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

        /// <summary>
        /// Creates new products
        /// </summary>
        /// <param name="commands">List of commands to create products</param>
        /// <returns>The created products</returns>
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

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">Product identifier</param>
        /// <param name="command">Command with updated data</param>
        /// <returns>The updated product</returns>
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

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">Identifier of the product to delete</param>
        /// <returns>Operation result</returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            await this.Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok();
        }
    }
}
