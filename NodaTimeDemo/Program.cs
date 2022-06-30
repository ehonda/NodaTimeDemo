using NodaTime;
using NodaTime.Extensions;
using NodaTime.Text;

var start = LocalDateTimePattern.GeneralIso.Parse("2022-06-29T23:10:00").Value;

// var zone = DateTimeZoneProviders.Tzdb["Europe/Berlin"];
// var clock = SystemClock.Instance.InZone(zone);
// var end = clock.GetCurrentLocalDateTime();

var utc = OffsetDateTimePattern.Rfc3339.Parse("2022-06-29T21:25:00Z").Value;
var zone = DateTimeZoneProviders.Tzdb["Europe/Berlin"];
var end = utc.InZone(zone).LocalDateTime;

var diffPeriod = Period.Between(start, end, PeriodUnits.Hours | PeriodUnits.Minutes | PeriodUnits.Seconds);
var diffDuration = diffPeriod.ToDuration();

Console.WriteLine($"Start is {start}");
Console.WriteLine($"End is {end}");
Console.WriteLine($"Diff period is {diffPeriod}");
Console.WriteLine($"Diff duration is {diffDuration}");

// TODO: Test effects of dst transitions etc
