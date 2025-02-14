using EPiServer.Commerce.Catalog.ContentTypes;

namespace Optimizely.Demo.Commerce.Core.Extensions;

public static class VariationContentExtensions
{
    public static string? GetProductId(this VariationContent variant)
    {
        if (variant is null)
            return string.Empty;

        return variant.Code.Split('-').LastOrDefault();
    }
}
