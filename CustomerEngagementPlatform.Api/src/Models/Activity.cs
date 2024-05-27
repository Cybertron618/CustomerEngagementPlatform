namespace CustomerEngagementPlatform.Api.src.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public int CustomerId { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public DateTime ActivityDate { get; set; }

        // Navigation property
        public Customer? Customer { get; set; }
    }
}

