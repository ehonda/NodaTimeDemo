using JetBrains.Annotations;
using NodaTime;
using NodaTime.Extensions;

namespace Common.Extensions;

[PublicAPI]
public static class DateTimeExtensions
{
    public static Duration GetDurationUntil(this DateTime start, ZonedDateTime end)
    {
        var startInZone = start.ToLocalDateTime().InZoneStrictly(end.Zone);
        return end - startInZone;
    }
}
