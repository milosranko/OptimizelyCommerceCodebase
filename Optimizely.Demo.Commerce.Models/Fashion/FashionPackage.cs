using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Commerce.Demo.Models;

[CatalogContentType(
    GUID = "0E2BA809-EAE7-463F-904D-B2F43C0B4FC1",
    DisplayName = "Fashion Package",
    MetaClassName = "FashionPackage",
    Description = "Displays a package, which is comparable to an individual SKU because Package item must be purchased as a whole.")]
public class FashionPackage : PackageContent
{
    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Description", Order = 1)]
    public virtual XhtmlString Description { get; set; }
}
