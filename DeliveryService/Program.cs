using DeliveryService.Deliveries;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App
var app = builder.Build();

app.UseHttpLogging();

app.MapGet("/deliveries", () => Task.FromResult(Current.Deliveries));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: Re-enable and make calls from Dispatch work with https
// app.UseHttpsRedirection();

app.Run();
