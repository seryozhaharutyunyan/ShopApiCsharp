using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EdgeController : ControllerBase
    {
        public readonly IEdgeRepository repository;

        public EdgeController(IEdgeRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Edge> edges = await repository.RetrieveAllAsync();

            return Ok(edges);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Edge? edge = await repository.RetrieveAsync(id);

            if (edge is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(edge);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Edge data)
        {
            Edge? edge = await repository.CreateAsync(data);
            if (edge is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(edge);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Edge data)
        {
            Edge? edge = await repository.UpdateAsync(id, data);

            if(edge is null)
            {
                return new NoContentResult();
            }

            return Ok(edge);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool? flag = await repository.DeleteAsync(id);
            if (flag == false || flag is null)
            {
                return new NoContentResult();
            }

            return Ok();
        }
    }
}
