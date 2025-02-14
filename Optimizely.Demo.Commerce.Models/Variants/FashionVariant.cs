using EPiServer.Commerce.Catalog.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Optimizely.Commerce.Demo.Infrastructure.Constants;
using Optimizely.Demo.Commerce.Models.Variants.Base;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Demo.Commerce.Models.Variants;

[CatalogContentType(
    GUID = "D27B133B-F9C5-438C-BC51-F4DE632BFC17",
    MetaClassName = MetaClassNames.FashionVariant,
    GroupName = GroupNames.Fashion,
    DisplayName = "Fashion variant",
    Description = "Display fashion variant")]
//[ImageUrl("~/Templates/thumbnail-fashion.png")]
public class FashionVariant : VariantBase
{
    [Searchable]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Size", Order = 10)]
    public virtual string Size { get; set; }

    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Color", Order = 20)]
    public virtual string Color { get; set; }

    [Searchable]
    [CultureSpecific]
    [Tokenize]
    [IncludeInDefaultSearch]
    [Display(Name = "Description", Order = 30)]
    public virtual XhtmlString Description { get; set; }
}
