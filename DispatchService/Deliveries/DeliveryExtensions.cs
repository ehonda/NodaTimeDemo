using Common.Deliveries.Dtos;
using Common.Extensions;
using DispatchService.Dispatches;
using NodaTime;
using NodaTime.Extensions;

namespace DispatchService.Deliveries;

public static class DeliveryExtensions
{
    public static DispatchDto Dispatch(this DeliveryDto delivery, string timeZoneId = "Europe/Berlin")
    {
        var zone = DateTimeZoneProviders.Tzdb[timeZoneId];
        var clock = SystemClock.Instance.InZone(zone);
        var now = clock.GetCurrentLocalDateTime().InZoneLeniently(zone);

        var readyFor = delivery.PreparedAt.GetDurationUntil(now);
        var driver = AssignDriver();

        return new(delivery, readyFor, driver);
        
        string AssignDriver()
        {
            if (readyFor >= Duration.FromHours(1))
                return "Rocket man";

            if (readyFor >= Duration.FromMinutes(30))
                return "Surfer boy";

            return "Slowpoke";
        }
    }
}
