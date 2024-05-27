using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerEngagementPlatform.Api.src.Models;

namespace CustomerEngagementPlatform.Api.src.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllActivitiesAsync();
        Task<Activity?> GetActivityByIdAsync(int activityId);
        Task AddActivityAsync(Activity activity);
        Task UpdateActivityAsync(Activity activity);
        Task DeleteActivityAsync(int activityId);
    }
}
