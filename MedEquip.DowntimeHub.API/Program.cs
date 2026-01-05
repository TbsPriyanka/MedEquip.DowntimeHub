using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigin", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
    });
});
#endregion

#region Connection String

var environment = builder.Environment;
var connectionString = environment.IsProduction()
    ? builder.Configuration.GetConnectionString("MedEquipConnectionLive")
    : builder.Configuration.GetConnectionString("MedEquipConnection");
Log.Information("ASPNETCORE_ENVIRONMENT = {Env}", builder.Environment.EnvironmentName);

builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));
Log.Information("Using connection string: {Connection}", connectionString);

#endregion

app.UseHttpsRedirection();

app.Run();