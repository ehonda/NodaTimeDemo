using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.Extensions;
using FluentAssertions;
using NodaTime;
using NodaTime.Testing.TimeZones;
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

    // TODO: Create date time values from pattern
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

    // TODO: Create date time values from pattern
    // TODO: Refine description and name, what should happen?
    [Test]
    [Description("Duration is calculated from a date time value in a backward transition, i.e. ambiguous.")]
    public void Backward_transition()
    {
        var transition = Instant.FromUtc(
            2022, 01, 01,
            01, 00);
        var zone = new SingleTransitionDateTimeZone(transition, 0, -1);

        var start = new DateTime(
            2022, 01, 01,
            00, 30, 00);
        var end = new LocalDateTime(
            2022, 01, 01,
            01, 30, 00).InZoneLeniently(zone);
        
        start.GetDurationUntil(end).Should().Be(Duration.FromHours(2));
    }
    
    // TODO: Create date time values from pattern
    // TODO: Refine description and name, what should happen?
    [Test]
    [Description("Duration is calculated from a date time value in a forward transition, i.e. non existent.")]
    public void Forward_transition()
    {
        var transition = Instant.FromUtc(
            2022, 01, 01,
            01, 00);
        var zone = new SingleTransitionDateTimeZone(transition, 0, 1);

        var start = new DateTime(
            2022, 01, 01,
            01, 30, 00);
        var end = new LocalDateTime(
            2022, 01, 01,
            03, 00, 00).InZoneLeniently(zone);
        
        start.GetDurationUntil(end).Should().Be(Duration.FromMinutes(30));
    }
}