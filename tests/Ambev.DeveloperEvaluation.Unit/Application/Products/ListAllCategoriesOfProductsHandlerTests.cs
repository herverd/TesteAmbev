using Ambev.DeveloperEvaluation.Application.Products.ListAllCategoriesOfProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="ListAllCategoriesOfProductsHandler"/> class.
/// </summary>
public class ListAllCategoriesOfProductsHandlerTests
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ListAllCategoriesOfProductsHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListAllCategoriesOfProductsHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public ListAllCategoriesOfProductsHandlerTests()
    {
        _categoryRepository = Substitute.For<ICategoryRepository>();
        _handler = new ListAllCategoriesOfProductsHandler(_categoryRepository);
    }

    /// <summary>
    /// Tests that products are listed.
    /// </summary>
    [Fact(DisplayName = "Given two produts when handle result should have count two")]
    public async Task Given_Two_Products_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new ListAllCategoriesOfProductsCommand();

        ICollection<string> categoryNames = new Faker().Commerce.Categories(5);
        _categoryRepository.ListAllCategoriesBeingUsedAsync(Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(categoryNames));

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should()
            .NotBeNull()
            .And
            .HaveCount(5)
            .And
            .ContainInOrder(categoryNames);
    }
}
