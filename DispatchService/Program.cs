using Common.Deliveries;
using DispatchService.Deliveries;
using DispatchService.Dispatches;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Any;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);

// Services
// -----------------------------------------------------------------------------------------------

// Delivery service client
// See https://restsharp.dev/v107/#recommended-usage on why to use singleton
builder.Services.AddSingleton<DeliveryServiceClient>();

// ApiExplorer for swagger generation
builder.Services.AddEndpointsApiExplorer();

// Configuration of Duration serialization
builder.Services.Configure<JsonOptions>(options =>
    options.SerializerOptions.Converters.Add(NodaConverters.DurationConverter));
builder.Services.AddSwaggerGen(options =>
    options.MapType<Duration>(() => new()
    {
        Type = "string",
        Example = new OpenApiString("1:00:00.8188826"),
        Description = "Standard round-trip pattern `j` for serialization is used, " +
                      "see <https://nodatime.org/3.1.x/userguide/duration-patterns>"
    }));

// App
// -----------------------------------------------------------------------------------------------

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

app.Run();