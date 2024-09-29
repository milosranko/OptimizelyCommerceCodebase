using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Commerce.SpecializedProperties;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Optimizely.Commerce.Demo.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Commerce.Demo.Models;

[CatalogContentType(
    GUID = "74152223-99F0-4B3F-8FD0-DDCD535EC861",
    MetaClassName = Infrastructure.Constants.MetaClassNames.FashionProduct,
    GroupName = GroupNames.Fashion,
    DisplayName = "Fashion product",
    Description = "Fashion product with Add to Cart button.")]
//[ImageUrl("~/Templates/thumbnail-fashion.png")]
public class FashionProduct : ProductContent
{
    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Brand", Order = 10)]
    public virtual string Brand { get; set; }

    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Description", Order = 20)]
    public virtual XhtmlString Description { get; set; }

    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Long Description", Order = 30)]
    public virtual XhtmlString LongDescription { get; set; }

    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Sizing", Order = 40)]
    public virtual XhtmlString Sizing { get; set; }

    [CultureSpecific]
    [Display(Name = "Product Teaser", Order = 50)]
    public virtual XhtmlString ProductTeaser { get; set; }

    [Searchable]
    [IncludeInDefaultSearch]
    [BackingType(typeof(PropertyDictionaryMultiple))]
    [Display(Name = "Available Sizes", Order = 60)]
    public virtual ItemCollection<string> AvailableSizes { get; set; }

    [Searchable]
    [IncludeInDefaultSearch]
    [BackingType(typeof(PropertyDictionaryMultiple))]
    [Display(Name = "Available Colors", Order = 70)]
    public virtual ItemCollection<string> AvailableColors { get; set; }
}
