using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookingProcessorService.Infrastructure.HttpClients
{
    public class BookingProcessorServiceClient(HttpClient httpClient)
    {
        public async Task<BookingConfirmationResponse> ConfirmBooking(Guid bookingId)
        {
            HttpResponseMessage response = await httpClient
                .GetAsync($"api/supplier/confirmBooking/{bookingId}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException("Booking not found");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new HttpRequestException("Bad Request", null, System.Net.HttpStatusCode.BadRequest);
                }
                else
                {
                    throw new HttpRequestException($"Http Request Failed with status code {response.StatusCode}");
                }
            }
            BookingConfirmationResponse? ConfirmResponse = await
            response.Content.ReadFromJsonAsync<BookingConfirmationResponse>();

            if(ConfirmResponse == null)
            {
                throw new ArgumentException("Confirmation response is null");
            }

            return ConfirmResponse;
        }

    }
}
