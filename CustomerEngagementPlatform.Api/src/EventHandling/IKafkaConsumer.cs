using System;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerEngagementPlatform.Api.src.EventHandling
{
    public interface IKafkaConsumer
    {
        void Consume<T>(Func<T, Task> handleMessage, CancellationToken cancellationToken);
    }
}

