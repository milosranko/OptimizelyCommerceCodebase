using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Commerce.Demo.Models;

[CatalogContentType(
    GUID = "9A374ED1-AC7E-4329-B13D-6BB9A0344B79",
    DisplayName = "Fashion Bundle",
    MetaClassName = "FashionBundle",
    Description = "Displays a bundle, which is collection of individual fashion variants.")]
public class FashionBundle : BundleContent
{
    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Description", Order = 1)]
    public virtual XhtmlString Description { get; set; }
}
