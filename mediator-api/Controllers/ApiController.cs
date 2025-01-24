using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace webapi_docker.Controllers
{
    /// <summary>
    /// Base controller class that provides access to MediatR
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        /// <summary>
        /// Gets the MediatR mediator instance
        /// </summary>
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
