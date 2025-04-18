using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications;

public class SaleLimitReachedSpecificationTeste
{
    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("#(*")]
    [InlineData("12c")]
    public void Given_Invalid_Configuration_When_New_Specification_Maximum_Items_Per_Product_Should_Be_Twenty(
        string maximumItemPerProductsConfiguration)
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["Business:Rules:Sales:MaximumItemPerProducts"] = maximumItemPerProductsConfiguration,
            })
            .Build();

        // Act
        var specification = new SaleLimitReachedSpecification(configuration);

        // Assert
        specification.MaximumItemsPerProduct.Should().Be(20);
    }

    [Theory]
    [InlineData(1, 2, true)]
    [InlineData(1, 1, false)]
    [InlineData(20, 20, false)]
    [InlineData(20, 21, true)]
    public void Satisfied_Sale_Limit_Reached_As_Expected(
        int maximumItemPerProducts,
        int quantity,
        bool expectedSatisfied)
    {
        // Arrange
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["Business:Rules:Sales:MaximumItemPerProducts"] = maximumItemPerProducts.ToString(),
            })
            .Build();

        var customer = UserTestData.GenerateValidUser();
        var product = ProductTestData.GenerateValid();
        var cart = CartTestData.GenerateValid();
        cart.AddItems(
            CartItem.CreateForProduct(product, quantity, customer));
        var specification = new SaleLimitReachedSpecification(configuration);

        // Act
        var result = specification.IsSatisfiedBy(cart);

        // Assert
        result.Should().Be(expectedSatisfied);
    }
}
