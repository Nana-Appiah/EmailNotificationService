using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using Marathon.Model;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

string baseURI = @"http://10.150.0.4:5001/v1/MailSender";

app.MapPost("/marathon/sendNotification", async (Message msg) =>
{
    //sends a notification to list of people
    try
    {
        var stringPayload = JsonConvert.SerializeObject(msg);
        var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        //client configuration
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(baseURI);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.PostAsync(baseURI, content);

        if (response.IsSuccessStatusCode)
        {
            return Results.Ok(msg);
        }
        else
        {
            return Results.BadRequest();
        }
        
    }
    catch (Exception ex)
    {
        return Results.BadRequest();
    }

}).WithName("SendNotification");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

