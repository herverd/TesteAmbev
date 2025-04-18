using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CategoryTestData
{
    private static readonly Faker<Category> CategoryFaker = new Faker<Category>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.Name, f => f.Commerce.Categories(1).First());

    public static Category GenerateValid()
    {
        return CategoryFaker.Generate();
    }
}