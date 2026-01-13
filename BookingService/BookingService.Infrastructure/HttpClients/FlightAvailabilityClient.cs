using Azure;
using Shared.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Infrastructure.HttpClients
{
    public class FlightAvailabilityClient(HttpClient httpClient)
    {
        public async Task<AvailabilityResponse> IsFlightAvailableAsync(Guid flightId, int passengersCount)
        {
            HttpResponseMessage response = await httpClient.GetAsync(
                $"/api/supplier/availability/{flightId}?count={passengersCount}");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException("Availability not found");
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
            AvailabilityResponse? availabilityResponse = await
            response.Content.ReadFromJsonAsync<AvailabilityResponse>();

            if (availabilityResponse == null)
            {
                throw new ArgumentException("Confirmation response is null");
            }

            return availabilityResponse;
        }

    }
}
