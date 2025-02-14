using EPiServer.Commerce.Order;
using EPiServer.Commerce.Order.Internal;
using EPiServer.ServiceLocation;
using Mediachase.Commerce;

namespace Optimizely.Demo.Commerce.Core.Shipping;

[ServiceConfiguration(typeof(IShippingCalculator), Lifecycle = ServiceInstanceScope.Transient)]
public sealed class AnaShippingCalculator : IShippingCalculator
{
    public Money GetShippingCost(IOrderGroup orderGroup, IMarket market, Currency currency)
    {
        return GetShippingCost(orderGroup.GetFirstShipment(), market, currency);
    }

    public Money GetShippingCost(IOrderForm orderForm, IMarket market, Currency currency)
    {
        return GetShippingCost(orderForm.Shipments.FirstOrDefault(), market, currency);
    }

    public Money GetShippingCost(IShipment shipment, IMarket market, Currency currency)
    {
        if (shipment is null) return new Money(0, currency);

        var shippingGateway = new ShippingPlugin();
        var message = string.Empty;
        var rate = shippingGateway.GetRate(market, shipment.ShippingMethodId, shipment, ref message);

        return rate.Money;
    }

    public Money GetDiscountedShippingAmount(IShipment shipment, IMarket market, Currency currency)
    {
        //Unused in this app so returns zero
        return new Money(0, currency);
    }

    public Money GetShippingItemsTotal(IShipment shipment, Currency currency)
    {
        if (shipment == null)
            return new Money(0, currency);

        var shippingChargeableTotal = shipment.LineItems.Sum(i => i.GetExtendedPrice(currency).Amount);

        return new Money(shippingChargeableTotal, currency);
    }

    public Money GetShippingReturnItemsTotal(IShipment shipment, Currency currency)
    {
        //Unused in this app so returns zero
        return new Money(0, currency);
    }

    public Money GetShipmentDiscountPrice(IShipment shipment, Currency currency)
    {
        var shipmentData = shipment as SerializableShipment;

        return shipmentData == null
            ? new Money(0, currency)
            : new Money(shipmentData.ShipmentDiscount, currency);
    }

    public ShippingTotals GetShippingTotals(IShipment shipment, IMarket market, Currency currency)
    {
        if (shipment != null)
        {
            Money shippingTax = this.GetShippingTax(shipment, market, currency);
            Money shippingCost = this.GetShippingCost(shipment, market, currency);
            Money shippingItemsTotal = this.GetShippingItemsTotal(shipment, currency);
            Dictionary<ILineItem, LineItemPrices> lineitemprices = shipment.LineItems.ToDictionary(x => x, x => x.GetLineItemPrices(currency));
            return new ShippingTotals(shippingItemsTotal, shippingCost, shippingTax, lineitemprices);
        }

        return null;
    }

    public Money GetShippingTax(IShipment shipment, IMarket market, Currency currency)
    {
        //Unused in this app so returns zero
        return new Money(0, currency);
    }

    public Money GetSalesTax(IShipment shipment, IMarket market, Currency currency)
    {
        //Unused in this app so returns zero
        return new Money(0, currency);
    }

    public Money GetReturnShippingTax(IShipment shipment, IMarket market, Currency currency)
    {
        //Unused in this app so returns zero
        return new Money(0, currency);
    }

    public Money GetReturnSalesTax(IShipment shipment, IMarket market, Currency currency)
    {
        //Unused in this app so returns zero
        return new Money(0, currency);
    }

    public ShippingTotals GetReturnShippingTotals(IShipment shipment, Currency currency)
    {
        return null;
    }
}
