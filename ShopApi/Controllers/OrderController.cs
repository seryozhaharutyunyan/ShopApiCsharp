using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly IOrderRepository repository;

        public OrderController(IOrderRepository repository)
        {
            this.repository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Order> orders = await repository.RetrieveAllAsync();

            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Order? order = await repository.RetrieveAsync(id);

            if (order is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Order data)
        {
            Order? order = await repository.CreateAsync(data);
            if (order is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(order);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order data)
        {
            Order? order = await repository.UpdateAsync(id, data);

            if(order is null)
            {
                return new NoContentResult();
            }

            return Ok(order);
        }

        [Authorize]
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
