using Application.Extensions;
using Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["Connections:Database"];

builder.Host.UseSerilog((context, config) => config
    .ReadFrom.Configuration(context.Configuration));

builder.Services.AddFluentMigrator(connectionString);
builder.Services.AddDapper();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Services.UpdateDatabase();
app.Run();