using Ambev.DeveloperEvaluation.Common.Repositories;
using Ambev.DeveloperEvaluation.Common.Repositories.Pagination;
using AutoMapper;
using AutoMapper.Internal;

namespace Ambev.DeveloperEvaluation.Application.Common.Mapper;

/// <summary>
/// Extensions of AutoMapper.
/// </summary>
internal static class MapperExtensions
{
    /// <summary>
    /// Find first source property name.
    /// </summary>
    /// <param name="mapper">AutoMapper instance.</param>
    /// <param name="destinationPropertyName">Property name inside of mapping.</param>
    /// <param name="pairs">Pairs of mapps with yours relationship types.</param>
    /// <returns>Property name.</returns>
    /// <exception cref="InvalidOperationException">Occurs when not found TypeMap. This can happen when not mapping types using AutoMapper.</exception>
    public static string? FindFirstSourcePropertyName(this IMapper mapper, string destinationPropertyName, params TypePair[] pairs)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPropertyName, nameof(destinationPropertyName));
        ArgumentNullException.ThrowIfNull(pairs, nameof(pairs));

        var mapperConfiguration = mapper.ConfigurationProvider.Internal();

        var typeMap = pairs.Select(mapperConfiguration.FindTypeMapFor).FirstOrDefault()
            ?? throw new InvalidOperationException($"Not found map type from the pairs.");

        return typeMap.PropertyMaps.FirstOrDefault(pm => pm.DestinationMember?.Name.Equals(destinationPropertyName, StringComparison.OrdinalIgnoreCase) ?? false)?.SourceMember?.Name;
    }

    /// <summary>
    /// Mapping fields on pagination query command.
    /// </summary>
    /// <param name="mapper">AutoMapper instance.</param>
    /// <param name="query">Pagination query with sort fields.</param>
    /// <param name="pairs">Pairs of relationship.</param>
    /// <returns>New instance of <see cref="PaginationQuery"/> with mapped fields.</returns>
    public static PaginationQuery MapFrom(
        this IMapper mapper,
        PaginationQuery query,
        params TypePair[] pairs) =>
        mapper.MapFrom(query, null, pairs);

    public static PaginationQuery MapFrom(
        this IMapper mapper,
        PaginationQuery query,
        Func<string, TypePair, string?>? missingPropertyResolver = null,
        params TypePair[] pairs)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNull(pairs, nameof(pairs));

        int indexNestedSeparador;
        char nestingSeparator = '.';
        string sortPropetyName;
        string? translatedPropertyName;
        TypePair sortPropertiesMapping;
        Dictionary<string, SortDirection> translatedResult = [];

        foreach (var propertySort in query.Orders)
        {
            indexNestedSeparador = propertySort.Key.IndexOf(nestingSeparator);
            sortPropetyName = indexNestedSeparador < 0 ? propertySort.Key : propertySort.Key[(indexNestedSeparador + 1)..];
            sortPropertiesMapping = indexNestedSeparador < 0 ? pairs[0] : pairs[1];
            translatedPropertyName = mapper.FindFirstSourcePropertyName(sortPropetyName, sortPropertiesMapping);

            if (translatedPropertyName is null && missingPropertyResolver is not null)
            {
                translatedPropertyName = missingPropertyResolver(sortPropetyName, sortPropertiesMapping);
            }

            if (string.IsNullOrEmpty(translatedPropertyName))
            {
                throw CreateNewMissingException(sortPropetyName);
            }

            string accessorPropertyName = string.Empty;
            if (indexNestedSeparador > 0)
            {
                // note: translate relationship name.
                accessorPropertyName = propertySort.Key[..indexNestedSeparador];
                var navigationPropertyName = mapper.FindFirstSourcePropertyName(accessorPropertyName, pairs[0]);

                if (string.IsNullOrEmpty(navigationPropertyName))
                {
                    throw CreateNewMissingException(accessorPropertyName);
                }

                accessorPropertyName = navigationPropertyName + nestingSeparator;
            }

            translatedResult.Add(accessorPropertyName + translatedPropertyName, propertySort.Value);
        }

        return new PaginationQuery
        {
            Page = query.Page,
            Size = query.Size,
            Orders = translatedResult,
        };
    }

    private static MapperNotFoundPropertyException CreateNewMissingException(string propertyName) =>
        new($"Field '{propertyName}' not found.");
}
