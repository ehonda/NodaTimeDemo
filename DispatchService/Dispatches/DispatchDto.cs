using Common.Deliveries.Dtos;
using NodaTime;

namespace DispatchService.Dispatches;

public record DispatchDto(DeliveryDto Delivery, Duration ReadyFor, string Driver);
