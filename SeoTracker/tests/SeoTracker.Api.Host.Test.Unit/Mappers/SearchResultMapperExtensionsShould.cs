using FluentAssertions;
using FluentAssertions.Execution;

using SeoTracker.Api.Host.Mappers;
using SeoTracker.Test.Generators;

namespace SeoTracker.Api.Host.Test.Unit.Mappers;
public class SearchResultMapperExtensionsShould
{
    [Fact]
    public void Map_CorrectProperties_When_MappingSearchResult()
    {
        var dto = SearchRankDtoGenerator.Generate();

        var mapped = dto.MapToSearchResult();

        using var scope = new AssertionScope();
        mapped.Should().NotBeNull();
        mapped.Should().BeEquivalentTo(dto);
    }
}
