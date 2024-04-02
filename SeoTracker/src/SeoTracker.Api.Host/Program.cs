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