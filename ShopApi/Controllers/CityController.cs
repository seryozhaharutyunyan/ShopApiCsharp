using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        public readonly ICityRepository repository;

        public CityController(ICityRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<City> cities = await repository.RetrieveAllAsync();

            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            City? city = await repository.RetrieveAsync(id);

            if(city is null)
            {
                return new NoContentResult();
            }

            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] City data)
        {
            City? city = await repository.CreateAsync(data);

            if(city is null)
            {
                return new NoContentResult();
            }

            return Ok(city);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] City data)
        {
            City? city = await repository.UpdateAsync(id, data);

            if(city is null)
            {
                return new NoContentResult();
            }

            return Ok(city);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool? flag = await repository.DeleteAsync(id);

            if(flag is null || flag == false)
            {
                return new NoContentResult();
            } 

            return Ok();
        }
    }
}
