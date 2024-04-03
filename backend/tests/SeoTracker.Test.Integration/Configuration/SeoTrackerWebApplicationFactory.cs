using Microsoft.AspNetCore.Mvc.Testing;

namespace SeoTracker.Test.Integration.Configuration;
public class SeoTrackerWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public HttpClient Client { get; private set; } = default!;

    public Task InitializeAsync()
    {
        Client = CreateClient();
        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        Client.Dispose();
        return Task.CompletedTask;
    }
}
