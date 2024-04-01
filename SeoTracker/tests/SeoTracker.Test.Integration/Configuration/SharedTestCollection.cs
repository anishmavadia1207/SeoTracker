namespace SeoTracker.Test.Integration.Configuration;

[CollectionDefinition(nameof(SharedTestCollection))]
public class SharedTestCollection : ICollectionFixture<SeoTrackerWebApplicationFactory>
{
}
