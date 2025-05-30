using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripApi.Entities;
using TripApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly TripsService _tripService;
        public TripsController(TripsService tripService)
        {
            _tripService = tripService;
        }

        // GET: api/<TripsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _tripService.GetAllAsync());
        }

        // GET api/<TripsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var trip  = await _tripService.GetAsync(id);
            if (trip == null) { 
                return NotFound("Trip not found.");
            } 
            return Ok(trip);
        }

        // POST api/<TripsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TripEntity trip)
        {
            var idString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (idString == null || Guid.TryParse(idString, out Guid ownerId))
                return Unauthorized();

            if (await _tripService.CreateAsync(ownerId, trip)) { 
                return Ok();
            }

            return BadRequest("Failed to create trip.");
        }

        // PUT api/<TripsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TripEntity trip)
        {
            if (await _tripService.UpdateAsync(id, trip))
            {
                return Ok();
            }

            return BadRequest("Failed to update trip.");
        }

        // DELETE api/<TripsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _tripService.DeleteAsync(id))
            {
                return Ok();
            }

            return BadRequest("Failed to delete trip.");
        }
    }
}
