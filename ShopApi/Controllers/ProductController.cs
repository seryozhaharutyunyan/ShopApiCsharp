﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Interfaces;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Product> products = await repository.RetrieveAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product? product = await repository.RetrieveAsync(id);

            if (product is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Set([FromBody] Product data)
        {
            Product? product = await repository.CreateAsync(data);
            if (product is null)
            {
                return new NoContentResult(); ;
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product data)
        {
            Product? product = await repository.UpdateAsync(id, data);

            if(product is null)
            {
                return new NoContentResult();
            }

            return Ok(product);
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
