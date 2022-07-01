namespace DeliveryService.Deliveries;

public static class Current
{
    public static DeliveriesDto Deliveries
    {
        get
        {
            var local = DateTime.Now;
            // Unspecified date time is used so the DateTimes get serialized without specified utc offset
            // Component properties instead of ticks are used to have a nicer and more coarse representation 
            var unspecified = new DateTime(local.Year, local.Month, local.Day, local.Hour, local.Minute, local.Second,
                DateTimeKind.Unspecified);

            return new(new DeliveryDto[]
            {
                new(unspecified.AddHours(-1), "Pizza"),
                new(unspecified.AddMinutes(-30), "Spaghetti"),
                new(unspecified.AddMinutes(-1), "Garmonbozia")
            });
        }
    }
}