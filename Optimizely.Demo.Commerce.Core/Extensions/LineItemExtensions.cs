using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Order;
using EPiServer.ServiceLocation;
using Mediachase.Commerce.Catalog;

namespace Optimizely.Demo.Commerce.Core.Extensions;

public static class LineItemExtensions
{
    public static VariationContent? GetVariant(this ILineItem lineItem)
    {
        var referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        var variantLink = referenceConverter.GetContentLink(lineItem.Code);

        return contentLoader.TryGet<VariationContent>(variantLink, out var variant)
            ? variant
            : null;
    }

    public static bool HasTaxCategory(this ILineItem lineItem)
    {
        var category = lineItem.GetVariant().TaxCategoryId;

        return category != null && category == 1;
    }
}
