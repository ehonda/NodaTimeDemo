using Common.Deliveries.Dtos;
using RestSharp;

namespace Common.Deliveries;

public class DeliveryServiceClient
{
    private readonly RestClient _client = new("http://localhost:5180/");

    public Task<DeliveriesDto> GetDeliveries() => _client.GetAsync<DeliveriesDto>(new("deliveries"))!;
}
