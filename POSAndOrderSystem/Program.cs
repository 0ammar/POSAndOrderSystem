using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using POSAndOrderSystem.DbContexts;
using POSAndOrderSystem.Implementations;
using POSAndOrderSystem.Implemntations;
using POSAndOrderSystem.Interfaces;
using POSAndOrderSystem.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "My Resturant API",
		Version = "v1",
		Description = "An API to perform Admin, Cashier, and Customer behaviors",
		Contact = new OpenApiContact
		{
			Name = "Amma Arab",
			Email = "oammar@outlook.sa",
			Url = new Uri("https://0ammar.github.io/Personal-site/")
		}
	});
	// Set the comments path for the Swagger JSON and UI.
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);
});
// Connect my database with backend
builder.Services.AddDbContext<POSAndOrderContext>(con => con.UseSqlServer("Data Source=AMMAR-ARAB\\SQLEXPRESS;Initial Catalog=POSAndOrderSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"));

//Configure Serilog 
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
//string loggerPath = configuration.GetSection("LoggerPath").Value;
Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).
				WriteTo.File(Path.Combine(Directory.GetCurrentDirectory(), "Logs/POSLogging.txt"), rollingInterval: RollingInterval.Day).
				CreateLogger();
// Dependency injections
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ILookupServices, LookupServices>();
builder.Services.AddScoped<IMenuServices, MenuServices>();
builder.Services.AddScoped<IOrderServices, OrderService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json/", "My API Version 1");
	});
}

app.UseHttpsRedirection();

//Configure Static File
app.UseStaticFiles();
var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(imagesDirectory),
	RequestPath = "/Uploads"
});

app.UseAuthorization();
app.MapControllers();

// Start the application
try
{
	Log.Information("Start Running The API");
	Log.Information("App Runs Successfully");
	app.Run();

}
catch (Exception ex)
{
	Log.Information("An Error Occured");
	Log.Error($"Error {ex.Message} was Prevernt Application from run successfully");
}
finally
{
	Log.Warning("Test Warning");
}