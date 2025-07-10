using ApiRestWithNetCoreAndMongoDb.Repository;
using ApiRestWithNetCoreAndMongoDb.Settings;

var builder = WebApplication.CreateBuilder(args);

//bind with MongoDB

builder.Services.Configure<MongoDbSettings>(
builder.Configuration.GetSection(nameof(MongoDbSettings)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
