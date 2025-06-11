using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripApi.Entities;
using TripApi.Services;
using TripApi.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TripApi.Controllers
{
    [Route("[controller]")]
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
            var ownerId = UserUtils.getUserId(User);
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized();

            return Ok(await _tripService.GetAllAsync(ownerId));
        }

        // GET api/<TripsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var ownerId = UserUtils.getUserId(User);
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized();

            var trip = await _tripService.GetAsync(ownerId, id);
            if (trip == null)
            {
                return NotFound("Trip not found.");
            }
            return Ok(trip);
        }

        // POST api/<TripsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TripEntity trip)
        {
            var ownerId = UserUtils.getUserId(User);
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized();

            if (await _tripService.CreateAsync(ownerId, trip))
            {
                return Ok();
            }

            return BadRequest("Failed to create trip.");
        }

        // PUT api/<TripsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TripEntity trip)
        {
            var ownerId = UserUtils.getUserId(User);
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized();

            if (await _tripService.UpdateAsync(ownerId, id, trip))
            {
                return Ok();
            }

            return BadRequest("Failed to update trip.");
        }

        // DELETE api/<TripsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ownerId = UserUtils.getUserId(User);
            if (string.IsNullOrWhiteSpace(ownerId))
                return Unauthorized();

            if (await _tripService.DeleteAsync(ownerId, id))
            {
                return Ok();
            }

            return BadRequest("Failed to delete trip.");
        }
    }
}
