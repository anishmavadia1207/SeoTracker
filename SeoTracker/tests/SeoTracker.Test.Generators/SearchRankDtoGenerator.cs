using Bogus;

using SeoTracker.Abstractions.Models;

namespace SeoTracker.Test.Generators;
public static class SearchRankDtoGenerator
{
    private static readonly Faker _faker = new();

    public static SearchRankDto Generate(
        string? engineName = null,
        string? url = null,
        string? query = null,
        int? rank = null) => new(
            engineName ?? _faker.Lorem.Word(),
            url ?? _faker.Internet.Url(),
            query ?? _faker.Lorem.Word(),
            rank ?? _faker.Random.Number(1, 1000));
}
