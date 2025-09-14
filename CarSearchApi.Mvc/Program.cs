using CarSearchApi.Mvc.Services;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Configure and register the Elasticsearch client (NEST)
// FIX: Throw a clear exception if the required configuration is missing.
var esUrl = builder.Configuration["Elasticsearch:Url"]
            ?? throw new InvalidOperationException("Configuration Error: Elasticsearch:Url is not set.");

var defaultIndex = builder.Configuration["Elasticsearch:DefaultIndex"]
                   ?? throw new InvalidOperationException("Configuration Error: Elasticsearch:DefaultIndex is not set.");

var settings = new ConnectionSettings(new Uri(esUrl))
    .DefaultIndex(defaultIndex)
    .EnableApiVersioningHeader();

var client = new ElasticClient(settings);
builder.Services.AddSingleton<IElasticClient>(client);
builder.Services.AddSingleton<ElasticsearchService>();

var app = builder.Build();

// 3. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 4. Seed the database on application startup
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider.GetRequiredService<ElasticsearchService>();

    Console.WriteLine("Creating index and seeding data...");
    await service.CreateIndexIfNotExistsAsync();
    await service.IndexSampleDataAsync();
    Console.WriteLine("Setup complete.");
}

app.Run();