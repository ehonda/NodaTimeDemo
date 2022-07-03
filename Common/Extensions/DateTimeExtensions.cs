using JetBrains.Annotations;
using NodaTime;
using NodaTime.Extensions;
using NodaTime.TimeZones;

namespace Common.Extensions;

[PublicAPI]
public static class DateTimeExtensions
{
    public static Duration GetDurationUntil(this DateTime start, ZonedDateTime end,
        ZoneLocalMappingResolver? resolver = null)
    {
        var startInZone = start.ToLocalDateTime().InZone(end.Zone, resolver ?? Resolvers.LenientResolver);
        return end - startInZone;
    }
}
