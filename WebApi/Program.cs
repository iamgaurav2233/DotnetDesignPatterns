using Microsoft.AspNetCore.DataProtection.Repositories;
using MyReposiotry;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(); // 👈 must be before Build()
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); // 👈 good to include if you plan to use [Authorize]

app.MapControllers();   // 👈 maps your StudentController and others

app.Run();
