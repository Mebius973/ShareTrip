using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripApi.Data.Models;
using TripApi.Entities;
using TripApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CommentariesController : ControllerBase
    {
        private readonly CommentariesService _commentariesService;
        public CommentariesController(CommentariesService commentariesService)
        {
            _commentariesService = commentariesService;
        }

        // GET: api/<CommentariesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _commentariesService.GetAllAsync());
        }

        // GET api/<CommentariesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var commentary = await _commentariesService.GetAsync(id);
            if (commentary == null)
            {
                return NotFound("Commentary not found.");
            }
            return Ok(commentary);
        }

        // POST api/<CommentariesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentaryEntity commentary)
        {
            if (await _commentariesService.CreateAsync(commentary))
            {
                return Ok();
            }

            return BadRequest("Failed to create commentary.");
        }

        // PUT api/<CommentariesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentaryEntity commentary)
        {
            if (await _commentariesService.UpdateAsync(id, commentary))
            {
                return Ok();
            }

            return BadRequest("Failed to update commentary.");
        }

        // DELETE api/<CommentariesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _commentariesService.DeleteAsync(id))
            {
                return Ok();
            }

            return BadRequest("Failed to delete commentary.");
        }
    }
}
