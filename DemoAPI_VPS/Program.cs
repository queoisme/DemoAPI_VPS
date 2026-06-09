using DemoAPI_VPS.Data;
using DemoAPI_VPS.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Demo API is running");

app.MapGet("/students", async(AppDbContext db) =>
{
    return await db.Students.ToListAsync();
});

app.MapPost("/students", async (Student s, AppDbContext db) =>
{
    db.Students.Add(s);
    await db.SaveChangesAsync();
    return Results.Created($"/students/{s.Id}", s);
});

app.MapGet("/students/{id:int}", async (int id, AppDbContext db) =>
{
    var student = await db.Students.FindAsync(id);
    return student is null ? Results.NotFound() : Results.Ok(student);
});
app.Run();
