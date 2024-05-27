using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerEngagementPlatform.Api.src.Models;
using CustomerEngagementPlatform.Api.src.Services;

namespace CustomerEngagementPlatform.Api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivitiesController(IActivityService activityService) : ControllerBase
    {
        private readonly IActivityService _activityService = activityService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetAllActivities()
        {
            var activities = await _activityService.GetAllActivitiesAsync();
            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivityById(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        [HttpPost]
        public async Task<ActionResult> AddActivity(Activity activity)
        {
            await _activityService.AddActivityAsync(activity);
            return CreatedAtAction(nameof(GetActivityById), new { id = activity.ActivityId }, activity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActivity(int id, Activity activity)
        {
            if (id != activity.ActivityId)
            {
                return BadRequest();
            }

            var existingActivity = await _activityService.GetActivityByIdAsync(id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            await _activityService.UpdateActivityAsync(activity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActivity(int id)
        {
            var existingActivity = await _activityService.GetActivityByIdAsync(id);
            if (existingActivity == null)
            {
                return NotFound();
            }

            await _activityService.DeleteActivityAsync(id);
            return NoContent();
        }
    }
}
