using JetBrains.Annotations;
using NodaTime;
using NodaTime.Extensions;

namespace Common.Extensions;

[PublicAPI]
public static class DateTimeExtensions
{
    // TODO: Better name
    public static Duration GetDurationUntil(this DateTime start, ZonedDateTime end)
    {
        var pastInZone = start.ToLocalDateTime().InZoneStrictly(end.Zone);
        return end - pastInZone;
    }
}