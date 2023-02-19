using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Middlewares;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "BookStore RESTful API",
        Contact = new OpenApiContact
        {
            Name = "Enes Arat",
            Url = new Uri("https://github.com/enesarat"),
            Email = "enes_arat@outlook.com"
        },
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<ILoggerService, ConsoleLogger>();


// Here, we define our tatabase context as "in memory database" before the build task of application
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Here, we initialize the data generator to seed data into "in memory database".
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataGenerator.Initialize(services);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddle();

app.MapControllers();

app.Run();
