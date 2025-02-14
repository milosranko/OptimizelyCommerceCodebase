using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;
using Optimizely.Demo.Commerce.Models.Bundles;
using Optimizely.Demo.Commerce.Models.Categories.Base;
using Optimizely.Demo.Commerce.Models.Packages;
using Optimizely.Demo.Commerce.Models.Products;

namespace Optimizely.Demo.Commerce.Models.Categories;

[CatalogContentType(
    GUID = "AAB498B1-FE77-4B30-A39D-9388A93C355E",
    MetaClassName = "FashionCategory",
    DisplayName = "Fashion Node",
    Description = "Display fashion products.")]
[AvailableContentTypes(Include =
    [
        typeof(FashionProduct),
        typeof(FashionPackage),
        typeof(FashionBundle),
        typeof(FashionCategory)
    ])]
public class FashionCategory : CategoryBase
{
}
