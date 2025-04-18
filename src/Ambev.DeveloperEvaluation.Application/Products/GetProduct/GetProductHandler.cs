using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Handler for processing <see cref="GetProductCommand"/> requests.
/// </summary>
public class GetProductHandler : IRequestHandler<GetProductCommand, ProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of <see cref="GetProductHandler"/>.
    /// </summary>
    /// <param name="productRepository">The product repository</param>
    /// <param name="unitOfWork">Unit of work</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public GetProductHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the <see cref="GetProductCommand"/> request.
    /// </summary>
    /// <param name="command">The GetProduct command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    public async Task<ProductResult> Handle(GetProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new GetProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (product is null)
            throw new NotFoundDomainException(BusinessRuleMessages.ProductNotFound(command.Id));

        return _mapper.Map<ProductResult>(product);
    }
}
