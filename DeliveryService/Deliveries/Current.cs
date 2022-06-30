namespace DeliveryService.Deliveries;

public static class Current
{
    public static DeliveriesDto Deliveries
    {
        get
        {
            var now = DateTime.Now;
            return new(new DeliveryDto[]
            {
                new(now.AddHours(-1), "Pizza"),
                new(now.AddMinutes(-30), "Spaghetti"),
                new(now.AddMinutes(-1), "Garmonbozia")
            });
        }
    }
}
