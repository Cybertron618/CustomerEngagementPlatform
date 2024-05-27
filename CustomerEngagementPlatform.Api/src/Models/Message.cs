using CustomerEngagementPlatform.Api.src.Models;

namespace CustomerEngagementPlatform.Api.src.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int CustomerId { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }

        // Navigation property
        public Customer? Customer { get; set; }
    }
}

