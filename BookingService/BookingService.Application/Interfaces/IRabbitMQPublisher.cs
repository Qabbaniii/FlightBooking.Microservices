using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces
{
    public interface IRabbitMQPublisher
    {
        Task InitializeAsync();
        Task Publish<T>(T message);
    }
}
