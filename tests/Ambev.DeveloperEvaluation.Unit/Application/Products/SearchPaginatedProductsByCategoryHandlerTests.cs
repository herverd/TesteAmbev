using Ambev.DeveloperEvaluation.Application.Products.SearchPaginatedProductsByCategory;
using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="SearchPaginatedProductsByCategoryHandler"/> class.
/// </summary>
public class SearchPaginatedProductsByCategoryHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly SearchPaginatedProductsByCategoryHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchPaginatedProductsByCategoryHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public SearchPaginatedProductsByCategoryHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new SearchPaginatedProductsByCategoryHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that an invalid request when try searching products throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given missing category name When try to search products Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new SearchPaginatedProductsByCategoryCommand
        {
            CategoryName = string.Empty,
        };

        // When
        var method = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await method.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Tests that products are listed.
    /// </summary>
    [Fact(DisplayName = "Given two produts when handle result should have count two")]
    public async Task Given_Two_Products_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new SearchPaginatedProductsByCategoryCommand
        {
            CategoryName = "Eletronics",
        };

        ICollection<Product> products =
        [
            ProductTestData.GenerateValid(),
            ProductTestData.GenerateValid(),
        ];

        _productRepository.SearchPaginatedByCategoryNameAsync(Arg.Any<string>(), Arg.Any<PaginationQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new PaginationQueryResult<Product>
            {
                Items = products,
                TotalItems = products.Count,
            }));

        _mapper.Map<SearchPaginatedProductsByCategoryResult>(Arg.Any<PaginationQueryResult<Product>>())
            .Returns(new SearchPaginatedProductsByCategoryResult
            {
                Items = [new(), new()],
                TotalItems = products.Count,
            });

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().HaveCount(2);
        queryResult.TotalItems.Should().Be(2);
    }
}
