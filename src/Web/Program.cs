using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string connectionString = "Data Source=greetings.db;";
builder.Services.AddSqlite<GreetingDbContext>(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/greetings", async (GreetingDbContext db) => await db.Greetings.ToListAsync())
   .WithName("GetAllGreetings");

app.MapPost("/greetings", async ([FromBody] Greeting greeting, GreetingDbContext db) =>
    {
        db.Greetings.Add(greeting);
        await db.SaveChangesAsync();

        return Results.Created($"/greetings/{greeting.Id}", greeting);
    })
    .WithName("AddGreeting");

app.MapGet("/greetings/{id}", async (int id, GreetingDbContext db) =>
    {
        return await db.Greetings.FindAsync(id)
            is Greeting greeting
                ? Results.Ok(greeting)
                : Results.NotFound();
    })
    .WithName("GetGreetingById");

app.Run();

public partial class Program { } // Required for Testing
