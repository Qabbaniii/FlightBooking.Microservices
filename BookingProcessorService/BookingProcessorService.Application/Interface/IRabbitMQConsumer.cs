using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProcessorService.Application.Interface
{
    public interface IRabbitMQConsumer
    {
        Task InitializeAsync();
        Task Consume();
        public void Dispose();
    }
}
