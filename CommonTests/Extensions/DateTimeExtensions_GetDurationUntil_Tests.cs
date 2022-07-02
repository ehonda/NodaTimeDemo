using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.Extensions;
using FluentAssertions;
using NodaTime;
using NUnit.Framework;
using DateTimeExtensions = Common.Extensions.DateTimeExtensions;

namespace CommonTests.Extensions;

[TestOf(nameof(DateTimeExtensions.GetDurationUntil))]
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Improved readability")]
public class DateTimeExtensions_GetDurationUntil_Tests
{
    private static class ValueSources
    {
        // TODO: Add UTC and UTC +/- offset to list
        public static IEnumerable<string> AllTzdbTimeZoneIds => DateTimeZoneProviders.Tzdb.Ids;
    }
    
    [Test]
    [Description("Duration is calculated from a date time value of arbitrary kind to a zoned date time value of " +
                 "arbitrary zone. The correct result should be returned.")]
    public void Duration_is_calculated_correctly_for_date_time_value_of_arbitrary_kind(
        [Values] DateTimeKind kind,
        [ValueSource(typeof(ValueSources), nameof(ValueSources.AllTzdbTimeZoneIds))] string timeZoneId)
    {
        var start = new DateTime(2022, 01, 01, 00, 00, 00, kind);
        var timeZone = DateTimeZoneProviders.Tzdb[timeZoneId];
        var end = new LocalDateTime(2022, 01, 01, 01, 00, 00).InZoneLeniently(timeZone);

        start.GetDurationUntil(end).Should().Be(Duration.FromHours(1));
    }
    
    // TODO: Test for time zone transition
}