using JetBrains.Annotations;

namespace Common.Deliveries.Dtos;

[PublicAPI]
public record DeliveriesDto(IReadOnlyCollection<DeliveryDto> Deliveries);
