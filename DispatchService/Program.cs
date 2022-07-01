using Common.Deliveries;
using Common.Deliveries.Dtos;
using DispatchService.Dispatches;
using NodaTime;
using NodaTime.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Services
// See https://restsharp.dev/v107/#recommended-usage on why to use singleton
builder.Services.AddSingleton<DeliveryServiceClient>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App
var app = builder.Build();

// TODO: Extract logic to static method
app.MapGet("/dispatches", async (DeliveryServiceClient client) =>
{
    var deliveriesDto = await client.GetDeliveries();

    var dispatches = deliveriesDto.Deliveries
        .Select(delivery =>
        {
            var dispatchData = Dispatch(delivery);
            return new DispatchDto(delivery, dispatchData.ReadyFor, dispatchData.Driver);
        });

    return new DispatchesDto(dispatches.ToArray());

    (Duration ReadyFor, string Driver) Dispatch(DeliveryDto delivery)
    {
        var zone = DateTimeZoneProviders.Tzdb["Europe/Berlin"];
        var clock = SystemClock.Instance.InZone(zone);
        var now = clock.GetCurrentLocalDateTime();

        var preparedAt = delivery.PreparedAt.ToLocalDateTime();
        // TODO: Only works for month / year components zero
        var readyFor = Period.Between(preparedAt, now).ToDuration();

        return (readyFor, AssignDriver(readyFor));
    }

    string AssignDriver(Duration readyFor)
    {
        if (readyFor >= Duration.FromHours(1))
            return "Rocket man";

        if (readyFor >= Duration.FromMinutes(30))
            return "Surfer boy";

        return "Slowpoke";
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
