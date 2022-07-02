using Common.Deliveries;
using DispatchService.Deliveries;
using DispatchService.Dispatches;

var builder = WebApplication.CreateBuilder(args);

// Services
// See https://restsharp.dev/v107/#recommended-usage on why to use singleton
builder.Services.AddSingleton<DeliveryServiceClient>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App
var app = builder.Build();

app.MapGet("/dispatches", async (DeliveryServiceClient client) =>
{
    var deliveriesDto = await client.GetDeliveries();

    return deliveriesDto.Deliveries
        .Select(delivery => delivery.Dispatch())
        .AsDto();
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