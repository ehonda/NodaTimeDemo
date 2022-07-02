﻿using System;
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
    [Test]
    [Description("Duration is calculated from a date time value of arbitrary kind to a zoned date time value of " +
                 "arbitrary zone. The correct result should be returned.")]
    public void Duration_is_calculated_correctly_for_date_time_value_of_arbitrary_kind(
        [Values] DateTimeKind kind,
        // TODO: Test against all time zones available in tzdb
        [Values("Europe/Berlin", "UTC", "UTC+02", "America/Los_Angeles")] string timeZoneId)
    {
        var start = new DateTime(2022, 01, 01, 00, 00, 00, kind);
        var timeZone = DateTimeZoneProviders.Tzdb[timeZoneId];
        var end = new LocalDateTime(2022, 01, 01, 01, 00, 00).InZoneLeniently(timeZone);

        start.GetDurationUntil(end).Should().Be(Duration.FromHours(1));
    }
}