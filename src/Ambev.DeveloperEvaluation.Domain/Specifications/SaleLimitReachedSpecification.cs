﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

/// <summary>
/// Spec that check SaleItems amount and SaleItems quantity individually
/// </summary>
public class SaleLimitReachedSpecification : ISpecification<Cart>
{
    private readonly int _maximumItemsPerProduct = 20;

    public SaleLimitReachedSpecification(IConfiguration configuration)
    {
        _maximumItemsPerProduct = int.TryParse(configuration["Business:Rules:Sales:MaximumItemPerProducts"], out var value) ? value : _maximumItemsPerProduct;
    }

    public int MaximumItemsPerProduct => _maximumItemsPerProduct;

    public bool IsSatisfiedBy(Cart cart)
    {
        return cart.Items
            .Any(i => i.Quantity > _maximumItemsPerProduct);
    }
}