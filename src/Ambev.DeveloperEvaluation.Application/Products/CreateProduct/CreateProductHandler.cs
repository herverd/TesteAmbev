using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing <see cref="CreateProductCommand"/> requests.
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly EnsureCategoryService _ensureCategoryService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="ensureCategoryService">The category service</param>
    /// <param name="unitOfWork">Unit of work.</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateProductHandler(
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
    /// Handles the CreateProductCommand request
    /// </summary>
    /// <param name="command">The CreateProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product details</returns>
    public async Task<ProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);
        if (existingProduct != null)
            throw new DomainException(BusinessRuleMessages.ProductTitleExists(command.Title).Detail);

        var product = _mapper.Map<Product>(command);

        product.Category = await _ensureCategoryService.EnsureCategoryNameAsync(command.CategoryName, cancellationToken);

        _productRepository.Create(product);
        await _unitOfWork.ApplyChangesAsync(cancellationToken);

        return _mapper.Map<ProductResult>(product);
    }
}
