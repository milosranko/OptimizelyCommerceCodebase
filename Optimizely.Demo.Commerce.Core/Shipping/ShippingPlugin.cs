using EPiServer.Commerce.Order;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using Mediachase.Commerce.Orders.Managers;

namespace Optimizely.Demo.Commerce.Core.Shipping;

[ServiceConfiguration(typeof(IShippingPlugin), Lifecycle = ServiceInstanceScope.Transient)]
public sealed class ShippingPlugin : IShippingPlugin
{
    public ShippingPlugin()
    {
    }

    public ShippingRate GetRate(IMarket market, Guid methodId, IShipment shipment, ref string message)
    {
        var currency = market.DefaultCurrency;

        if (methodId == Guid.Empty)
            return new ShippingRate(methodId, "No shipping method", new Money(0, currency));

        //var shipmentLineItems = shipment.LineItems.ToList();
        //var shippingFreeItems = shipmentLineItems.Where(i => i.IsShippingFree()).ToList();

        ////calculate shipping cost for books separately
        //var ChargeableBookItems = shipmentLineItems.Where(i => i.IsShippingChargeable() && i.IsBook()).ToList();
        //var shippingChargeableItems = shipmentLineItems.Where(i => i.IsShippingChargeable() && !i.IsBook()).ToList();
        //var shippingAdditionalCost = shippingChargeableItems.Sum(i => i.GetAdditionalShippingCost());
        //var shippingMethod = ShippingManager.GetShippingMethod(methodId);

        //if (shippingMethod?.ShippingMethod == null || !shippingMethod.ShippingMethod.Any())
        //    return new ShippingRate(methodId, "Obsolete shipping method", new Money(0, currency));

        //var shippingMethodRow = shippingMethod.ShippingMethod[0];
        //var shippingrows = AnaShippingHelper.GetShippingRows(shippingMethod, "Rows");

        //if (shippingFreeItems.Count == shipmentLineItems.Count)
        //    return new ShippingRate(methodId, shippingMethodRow.DisplayName, new Money(0, currency));

        //var shippingChargeableItemsTotal = shippingChargeableItems.Sum(i => i.GetExtendedPrice(currency).Amount);
        //var shippingRow = shippingrows.FirstOrDefault(r => (r.CartValueStart == 0 || shippingChargeableItemsTotal > r.CartValueStart) && shippingChargeableItemsTotal <= r.CartValueEnd);

        //decimal shippingMethodPrice = 0.0M;

        //if (shippingRow == null)
        //    new Money(shippingChargeableItemsTotal, currency).ToString();
        //else
        //    shippingMethodPrice = shippingRow.ShippingPriceType.Equals("Percentage")
        //        ? shippingChargeableItemsTotal / 100 * shippingRow.ShippingPrice
        //        : shippingRow.ShippingPrice;

        //return new ShippingRate(methodId, shippingMethodRow.DisplayName, new Money(0, currency));
        return new ShippingRate(methodId, "No shipping method", new Money(0, currency));
    }

    public ShippingRate GetBulkPurchaseRate(IMarket market, Guid methodId, decimal quantity)
    {
        var currency = market.DefaultCurrency;

        if (methodId == Guid.Empty)
            return new ShippingRate(methodId, "No shipping method", new Money(0, currency));

        var shippingMethod = ShippingManager.GetShippingMethod(methodId);
        var shippingMethodRow = shippingMethod.ShippingMethod[0];

        //return new ShippingRate(methodId, shippingMethodRow.DisplayName, new Money(shippingBooksCost, currency));
        return new ShippingRate(methodId, "No shipping method", new Money(0, currency));
    }
}
