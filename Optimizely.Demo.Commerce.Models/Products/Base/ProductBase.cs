using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Mediachase.Commerce.Catalog;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Demo.Commerce.Models.Products.Base;

public abstract class ProductBase : ProductContent
{
    #region Metadata tab

    [Display(
        Name = "Meta Title",
        Description = "The title of the page to be added to the <title> tag. Defaults to 'Heading' or 'Page name' value plus site name if not set.",
        Order = 1)]
    [CultureSpecific]
    public virtual string MetaTitle { get; set; }

    [Display(
        Name = "Meta Keywords",
        Description = "The content of the meta keywords tag.",
        Order = 2)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string MetaKeywords { get; set; }

    [Display(
        Name = "Meta Description",
        Description = "The content of the meta description tag. Defaults to 'Description' value if not set.",
        Order = 3)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string MetaDescription { get; set; }

    [Display(
        Name = "Meta Robots",
        Description = "I am the text that helps with our SEO optimization. Specfically I help search engines find me.",
        Order = 4)]
    public virtual string MetaRobots { get; set; }

    [Display(
        Name = "Social Title",
        Description = "The title of the page to be shown on social media when the page is shared. Defaults to 'Heading' or 'Page name' value plus site name if not set.",
        Order = 5)]
    [CultureSpecific]
    [StringLength(255, ErrorMessage = "The input must be fewer than 255 characters length")]
    public virtual string SocialTitle { get; set; }

    [Display(
        Name = "Social Description",
        Description = "The description/synopsis of the page to be used on social media when the page is shared. Defaults to meta description.",
        Order = 6)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string SocialDescription { get; set; }

    [Display(
        Name = "Social Image",
        Description = "The image to use when the page is shared on social media. Defaults to the 'Listing Image' value if set or the Social Image value set within the site settings.",
        Order = 7)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference SocialImage { get; set; }

    [Display(
        Name = "Twitter Author",
        Description = "The twitter handle of the author of this page to be used when shared on twitter. Defaults to the 'Twitter handle' value set on the site settings page.",
        Order = 8)]
    [CultureSpecific]
    [ScaffoldColumn(false)]
    [StringLength(255, ErrorMessage = "The input must be fewer than 255 characters length")]
    public virtual string TwitterAuthor { get; set; }

    [Display(
        Name = "Hide Social Sharing",
        Description = "Whether to hide the AddThis social sharing widget on this page",
        Order = 9)]
    public virtual bool HideSocialSharing { get; set; }

    [Display(
        Name = "Canonical Link",
        Description = "The canonical link for this page. Defaults to the current page URL. N.B. In most instances this should not be set.",
        Order = 10)]
    [CultureSpecific]
    [StringLength(255, ErrorMessage = "The input must be fewer than 255 characters length")]
    public virtual string CanonicalLink { get; set; }

    [Display(
        Name = "",
        Description = "",
        Order = 11)]
    [CultureSpecific]
    public virtual string DisableIndexing { get; set; }

    [Display(
        Name = "Hide From XML Sitemap",
        Description = "If selected the page will not appear in the XML sitemap.",
        Order = 12)]
    [CultureSpecific]
    public virtual bool HideFromXmlSitemap { get; set; }

    [Display(
        Name = "Exclude From Site Search",
        Description = "If selected the page will not appear in the search results.",
        Order = 13)]
    public virtual bool ExcludeFromSearch { get; set; }

    #endregion

    #region Find Helpers

    public IEnumerable<int> ProductCategories
    {
        get
        {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentRepository>();
            var linksRepository = ServiceLocator.Current.GetInstance<IRelationRepository>();
            var referenceConverter = ServiceLocator.Current.GetInstance<ReferenceConverter>();
            var allRelations = linksRepository.GetParents<Relation>(ContentLink);
            var categories = allRelations.OfType<NodeRelation>().ToList();
            var parentCategories = new List<CatalogContentBase>();

            try
            {
                if (categories.Any())
                {
                    parentCategories.AddRange(categories
                        .Where(nodeRelation => nodeRelation.Parent != referenceConverter.GetRootLink())
                        .Select(nodeRelation => contentLoader.Get<CatalogContentBase>(nodeRelation.Parent, new LanguageSelector("en-US")))
                        .Where(parentCategory => parentCategory != null));
                }
                else if (ParentLink != null && ParentLink != referenceConverter.GetRootLink())
                {
                    var parentCategory = contentLoader.Get<CatalogContentBase>(ParentLink, new LanguageSelector("en-US"));
                    parentCategories.Add(parentCategory);
                }
            }
            catch (Exception ex)
            {
                //TODO Log error
                //Logger.Service.Log(Level.Error, ex.Message);
            }

            return parentCategories.Select(x => x.ContentLink.ID);
        }
    }

    #endregion


}
