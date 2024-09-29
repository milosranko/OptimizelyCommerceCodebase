using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.DataAnnotations;

namespace Optimizely.Commerce.Demo.Models;

[CatalogContentType(
    GUID = "AAB498B1-FE77-4B30-A39D-9388A93C355E",
    MetaClassName = "FashionNode",
    DisplayName = "Fashion Node",
    Description = "Display fashion products.")]
[AvailableContentTypes(Include = new[]
    {
        typeof(FashionProduct),
        typeof(FashionPackage),
        typeof(FashionBundle),
        typeof(FashionVariant),
        typeof(NodeContent)
    })]
public class FashionNode : NodeContent
{
}
