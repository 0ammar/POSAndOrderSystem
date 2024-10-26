using Microsoft.EntityFrameworkCore;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<POSAndOrderContext>(con => con.UseSqlServer("Data Source=AMMAR-ARAB\\SQLEXPRESS;Initial Catalog=POSAndOrderSystem;Integrated Security=True;Trust Server Certificate=True"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
