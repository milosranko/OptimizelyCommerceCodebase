using EPiServer.Commerce.Catalog.ContentTypes;
using Mediachase.Commerce;
using Optimizely.Demo.Commerce.Models.Products.Base;

namespace Optimizely.Demo.Commerce.Models.ViewModels;

public class VariantsViewModel //: IVariantsModel
{
    public string DisplayName { get; set; }
    public string ImageUrl { get; set; }
    public string Url { get; set; }
    public Money? DiscountedPrice { get; set; }
    public Money PlacedPrice { get; set; }
    public Money? DiscountedMemberPrice { get; set; }
    public Money MemberPrice { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public bool IsAvailable { get; set; }
    public VariationContent Variant { get; set; }
    public string PromotionName { get; set; }
    public string VariantType { get; set; }
    public ProductBase Product { get; set; }
}
