using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Catalog;
using Mediachase.Commerce.Pricing;

namespace Optimizely.Demo.Commerce.Core.Extensions;

public static class ProductContentExtensions
{
    public static bool HasVariants(this ProductContent product)
    {
        return product.GetVariants().Any();
    }

    public static decimal? GetListPrice(this ProductContent product)
    {
        var variants = product.GetVariants().ToList();

        if (variants.Count == 0) return decimal.MaxValue;

        var repository = ServiceLocator.Current.GetInstance<IContentRepository>();
        var variant = repository.Get<VariationContent>(variants.First());
        var priceService = ServiceLocator.Current.GetInstance<IPriceService>();
        var currentMarket = ServiceLocator.Current.GetInstance<ICurrentMarket>();
        var market = currentMarket.GetCurrentMarket();

        // In current setup, only one market (US) and one currency (USD) is active.
        // if there are multiple markets/us then we should get the market/currency from user selected value (e.g. Cookie).
        return priceService.GetDefaultPrice(
            market.MarketId,
            DateTime.Now,
            new CatalogKey(variant.Code),
            market.DefaultCurrency)?.UnitPrice.Amount;
    }
}
