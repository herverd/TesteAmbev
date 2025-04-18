using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CategoryTest
{
    [Fact]
    public void Given_Category_When_Change_Name_Should_Be_Updated()
    {
        // Arrange
        var category = CategoryTestData.GenerateValid();

        // Act
        category.Change("Modified category");

        // Assert
        category.Name.Should().Be("Modified category");
        category.UpdatedAt.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that validation fails when user properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid category data")]
    public void Given_InvalidCategoryData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var category = new Category
        {
            Name = "", // Invalid: empty
        };

        // Act
        var result = category.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
