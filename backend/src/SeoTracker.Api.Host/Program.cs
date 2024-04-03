using SeoTracker.Abstractions.Services;
using SeoTracker.Core.Services;
using SeoTracker.Core.Services.SearchEngines;

var builder = WebApplication.CreateBuilder(args);
const string HostCorsPolicy = "HostCorsPolicy";

var hosts = builder.Configuration.GetValue<string>("AllowedHosts")!.Split(',');

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
    options.AddPolicy(HostCorsPolicy, policy =>
        policy.SetIsOriginAllowed(origin =>
            hosts.Contains(new Uri(origin).Host))
        .AllowAnyHeader()
        .AllowAnyMethod()));

builder.Services.AddHttpClient(
    GoogleSearchEngineService.Name,
    client =>
    {
        client.BaseAddress = new Uri("https://www.google.com");
        client.DefaultRequestHeaders.Add(
            "User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36");
    });

builder.Services.AddScoped<ISearchEngineService, GoogleSearchEngineService>();
builder.Services.AddScoped<ISearchEngineManager, SearchEngineManager>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(HostCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();


// Exposed public marker.
public partial class Program { }