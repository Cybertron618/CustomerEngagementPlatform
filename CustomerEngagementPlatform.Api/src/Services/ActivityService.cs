using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerEngagementPlatform.Api.src.Repositories;
using CustomerEngagementPlatform.Api.src.Models;

namespace CustomerEngagementPlatform.Api.src.Services
{
    public class ActivityService(IActivityRepository activityRepository) : IActivityService
    {
        private readonly IActivityRepository _activityRepository = activityRepository;

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _activityRepository.GetAllActivitiesAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(int activityId)
        {
            return await _activityRepository.GetActivityByIdAsync(activityId);
        }

        public async Task AddActivityAsync(Activity activity)
        {
            await _activityRepository.AddActivityAsync(activity);
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            await _activityRepository.UpdateActivityAsync(activity);
        }

        public async Task DeleteActivityAsync(int activityId)
        {
            await _activityRepository.DeleteActivityAsync(activityId);
        }
    }
}
