using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact(DisplayName = "Product updated at should change not be null when change values")]
    public void Given_Product_When_Change_Info_Then_UpdatedAt_Should_Not_Be_Null()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        product.Change("Modified name", 1, "Modified description", "https://image.jpg", new(), new() { Name = "Modified category" });

        // Assert
        product.Title.Should().Be("Modified name");
        product.Price.Should().Be(1);
        product.Description.Should().Be("Modified description");
        product.Image.Should().Be("https://image.jpg");
        product.Category.Should().NotBeNull();
        product.Category.Name.Should().Be("Modified category");
        product.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Given_Product_When_Call_Same_Category_Name_Result_Should_Be_False()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        var isSameCategoryName = product.SameCategoryName("Modified name");

        // Assert
        isSameCategoryName.Should().BeFalse();
    }

    [Fact]
    public void Given_Product_When_Try_Set_Stock_Quantity_To_Negative_Should_Throw_Exception()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        Action action = () => product.SetStockQuantity(-1);

        // Assert
        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Quantity must not be negative value to the change quantity.*");
    }

    [Fact]
    public void Given_Product_When_Set_Stock_Quantity_Then_Stock_Quantity_And_Update_Date_Should_Be_Changed()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        product.SetStockQuantity(666);

        //Assert
        product.StockQuantity.Should().Be(666);
        product.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Given_Product_When_Try_Decrease_Quantity_To_Negative_Should_Throw_Exception()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        Action action = () => product.DecreaseQuantity(-1);

        // Assert
        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Quantity must be positive value to the decrease quantity.*");
    }

    [Fact]
    public void Given_Product_When_Try_Decrease_Quantity_To_Negative_Quantity_Should_Be_Throw_Exception()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        Action action = () => product.DecreaseQuantity(product.StockQuantity + 1);

        // Assert
        action.Should()
            .Throw<DomainException>()
            .WithMessage($"Stock quantity must not be negative in product '{product.Title}'.");
    }

    [Fact]
    public void Given_Product_When_Decrease_All_Quantities_Stock_Should_Be_Zero()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        product.DecreaseQuantity(product.StockQuantity);

        // Assert
        product.StockQuantity.Should().Be(0);
        product.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Given_Product_When_Increase_Quantity_Stock_Should_Be_Changed()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        var originalQuantity = product.StockQuantity;
        product.IncreaseQuantity(1);

        // Assert
        product.StockQuantity.Should().Be(originalQuantity + 1);
        product.UpdatedAt.Should().NotBeNull();
    }

    [Fact]
    public void Given_Product_When_Try_Increase_Negative_Value_Should_Throw_Exception()
    {
        // Arrange
        var product = ProductTestData.GenerateValid();

        // Act
        Action action = () => product.IncreaseQuantity(-1);

        // Assert
        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage($"Quantity must be positive value to the increase quantity.*");
    }

    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product();

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
