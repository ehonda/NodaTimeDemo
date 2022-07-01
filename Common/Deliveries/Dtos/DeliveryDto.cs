using JetBrains.Annotations;

namespace Common.Deliveries.Dtos;

[PublicAPI]
public record DeliveryDto(DateTime PreparedAt, string Meal);
