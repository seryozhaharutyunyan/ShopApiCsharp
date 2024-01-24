using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.Interfaces;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LikeController : ControllerBase
    {
        public readonly ILikeRepository repository;

        public LikeController(ILikeRepository repository)
        {
            this.repository = repository;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Attach([FromForm] Like data)
        {
            return await repository.Attach(data) ? Ok() : BadRequest();
        }
    }
}
