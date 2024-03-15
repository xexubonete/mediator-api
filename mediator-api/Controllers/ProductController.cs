using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
        [Route("[action]")]
        public async Task<ActionResult<IList<Product>>> GetAllProducts() 
        {
            var result = await this.Mediator.Send(new GetAllProductsQuery());

            return Ok(result);
        }

        /// <summary>Gets the product by identifier.</summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        [Route("[action]/{Id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid Id)
        {
            var result = await this.Mediator.Send(new GetProductByIdQuery { Id = Id });

            return Ok(result);
        }

        /// <summary>Gets the name of the product by.</summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpGet]
        [Route("/{Name}")]
        public async Task<ActionResult<Product>> GetProductByName(string Name)
        {
            var result = await this.Mediator.Send(new GetProductByNameQuery { Name = Name });

            return Ok(result);
        }

        /// <summary>Creates the product.</summary>
        /// <param name="command">The command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        [Route("/create")]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductCommand command)
        {
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        /// <summary>Updates the product.</summary>
        /// <param name="command">The command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPut]
        [Route("/{Id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Guid Id, UpdateProductCommand command)
        {
            if (Id != command.Id)
            {
                return BadRequest();
            }
            var result = await this.Mediator.Send(command);

            return Ok(result);
        }

        /// <summary>Deletes the product.</summary>
        /// <param name="command">The command.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpDelete]
        [Route("/{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(Guid id)
        {
            await this.Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok();
        }
    }
}
