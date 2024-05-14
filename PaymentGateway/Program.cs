using PaymentGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddScopedServices();

var app = builder.Build();

app.AddUsings();
app.MapControllers();
app.Run();