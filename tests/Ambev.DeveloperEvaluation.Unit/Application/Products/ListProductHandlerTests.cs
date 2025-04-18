using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="ListProductHandler"/> class.
/// </summary>
public class ListProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ListProductHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListProductHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public ListProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListProductHandler(_productRepository, _mapper);
    }

    /// <summary>
    /// Tests that products are listed.
    /// </summary>
    [Fact(DisplayName = "Given two produts when handle result should have count two")]
    public async Task Given_Two_Products_When_Handle_Result_Should_Have_Two()
    {
        // Given
        var command = new ListProductCommand();

        ICollection<Product> products =
        [
            ProductTestData.GenerateValid(),
            ProductTestData.GenerateValid(),
        ];
        _mapper.Map<ListProductResult>(Arg.Any<PaginationQueryResult<Product>>()).Returns(new ListProductResult
        {
            Items = [new(), new()],
            TotalItems = products.Count,
        });
        _productRepository.PaginateAsync(Arg.Any<PaginationQuery>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(new PaginationQueryResult<Product>
            {
                Items = products,
                TotalItems = products.Count,
            }));

        // When
        var queryResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().HaveCount(2);
        queryResult.TotalItems.Should().Be(2);
    }
}
