using Microsoft.AspNetCore.Mvc;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController : ControllerBase
    {
        public readonly IRegionRepository repository;

        public RegionController(IRegionRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Region> regions = await repository.RetrieveAllAsync();

            return Ok(regions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Region? region = await repository.RetrieveAsync(id);

            if (region is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Region data)
        {
            Region? region = await repository.CreateAsync(data);
            if (region is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(region);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Region data)
        {
            Region? region = await repository.UpdateAsync(id, data);

            if(region is null)
            {
                return new NoContentResult();
            }

            return Ok(region);
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
