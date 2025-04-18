using Ambev.DeveloperEvaluation.Application.Products.ListAllCategoriesOfProducts;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListAllCategoriesOfProduct;

/// <summary>
/// Command for retrieving all categories of product.
/// </summary>
public class ListAllCategoriesOfProductsCommand : IRequest<ListAllCategoriesOfProductsResult>
{
}
