using NodaTime;
using NodaTime.Extensions;
using NodaTime.Text;

var start = LocalDateTimePattern.GeneralIso.Parse("2022-06-29T23:10:00").Value;

var zone = DateTimeZoneProviders.Tzdb["Europe/Berlin"];
var clock = SystemClock.Instance.InZone(zone);
var end = clock.GetCurrentLocalDateTime();

var diff = Period.Between(start, end);

Console.WriteLine($"Start is {start}");
Console.WriteLine($"End is {end}");
Console.WriteLine($"Diff is {diff.ToDuration()}");