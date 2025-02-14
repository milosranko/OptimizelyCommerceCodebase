using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Optimizely.Demo.Commerce.Models.Bundles.Base;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Demo.Commerce.Models.Bundles;

[CatalogContentType(
    GUID = "9A374ED1-AC7E-4329-B13D-6BB9A0344B79",
    DisplayName = "Fashion Bundle",
    MetaClassName = "FashionBundle",
    Description = "Displays a bundle, which is collection of individual fashion variants.")]
public class FashionBundle : BundleBase
{
    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Description", Order = 1)]
    public virtual XhtmlString Description { get; set; }
}
