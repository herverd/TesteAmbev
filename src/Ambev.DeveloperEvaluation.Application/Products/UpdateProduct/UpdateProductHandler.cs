using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing <see cref="UpdateProductCommand"/> requests.
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly EnsureCategoryService _ensureCategoryService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="UpdateProductHandler"/>.
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="ensureCategoryService">The category service</param>
    /// <param name="unitOfWork">Unit of work</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UpdateProductHandler(
        IProductRepository productRepository,
        EnsureCategoryService ensureCategoryService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _ensureCategoryService = ensureCategoryService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="UpdateProductCommand"/> request.
    /// </summary>
    /// <param name="command">The UpdateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    public async Task<ProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product is null)
            throw new NotFoundDomainException(BusinessRuleMessages.ProductNotFound(command.Id));

        var existingProduct = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);
        if (existingProduct is not null && existingProduct.Id != command.Id)
            throw new DomainException(BusinessRuleMessages.ProductTitleExists(command.Title).Detail);

        product.Change(command.Title, command.Price, command.Description, command.Image, command.Rating, null!);
        product.SetStockQuantity(command.Quantity);

        if (!product.SameCategoryName(command.CategoryName))
        {
            product.Category = product.Category = await _ensureCategoryService.EnsureCategoryNameAsync(command.CategoryName, cancellationToken);
        }

        await _unitOfWork.ApplyChangesAsync(cancellationToken);
        return _mapper.Map<ProductResult>(product);
    }
}
