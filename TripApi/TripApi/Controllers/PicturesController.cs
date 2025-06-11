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
    public class PicturesController : ControllerBase
    {
        private readonly PicturesService _picturesService;
        public PicturesController(PicturesService picturesService)
        {
            _picturesService = picturesService;
        }

        // GET: api/<PicturesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _picturesService.GetAllAsync());
        }

        // GET api/<PicturesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var picture = await _picturesService.GetAsync(id);
            if (picture == null)
            {
                return NotFound("Picture not found.");
            }
            return Ok(picture);
        }

        // POST api/<PicturesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PictureEntity picture)
        {
            if (await _picturesService.CreateAsync(picture))
            {
                return Ok();
            }

            return BadRequest("Failed to create picture.");
        }

        // PUT api/<PicturesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PictureEntity picture)
        {
            if (await _picturesService.UpdateAsync(id, picture))
            {
                return Ok();
            }

            return BadRequest("Failed to update picture.");
        }

        // DELETE api/<PicturesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _picturesService.DeleteAsync(id))
            {
                return Ok();
            }

            return BadRequest("Failed to delete picture.");
        }
    }
}
