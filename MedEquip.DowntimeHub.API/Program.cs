using MedEquip.DowntimeHub.API;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.WithAddSwaggerGen();

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

builder.Services.AddHttpContextAccessor();
builder.Services.RegisterServices();

#region Connection String

var environment = builder.Environment;
var connectionString = environment.IsProduction()
    ? builder.Configuration.GetConnectionString("MedEquipConnectionLive")
    : builder.Configuration.GetConnectionString("MedEquipConnection");

builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(connectionString));

#endregion

builder.Services.WithJwtSetting(builder.Configuration);
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

app.UseCors("AllowAllOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.Run();