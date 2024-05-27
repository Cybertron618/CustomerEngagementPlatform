using System.Threading.Tasks;

namespace CustomerEngagementPlatform.Api.src.EventHandling
{
    public interface IKafkaProducer
    {
        Task ProduceAsync<T>(T message);
    }
}

