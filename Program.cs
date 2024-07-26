using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/link/{idAgendamento}", ([SwaggerParameter] Guid idAgendamento) =>
{
    string url = "https://foursixhackathon.com.br?token=";

    using (var sha256 = SHA256.Create())
    {
        byte[] hash = sha256.ComputeHash(idAgendamento.ToByteArray());
        url += BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    return url;


    //var forecast = Enumerable.Range(1, 5).Select(index =>
    //    new WeatherForecast
    //    (
    //        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //        Random.Shared.Next(-20, 55),
    //        summaries[Random.Shared.Next(summaries.Length)]
    //    ))
    //    .ToArray();
    //return forecast;
})
.WithName("GetLinkTeleconsulta")
.WithOpenApi();

app.Run();
