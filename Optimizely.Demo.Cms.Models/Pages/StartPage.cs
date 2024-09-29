using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using Optimizely.Demo.ContentTypes.Blocks;
using Optimizely.Demo.ContentTypes.Constants;
using Optimizely.Demo.ContentTypes.Models.Pages;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Commerce.Demo.Models.Cms;

[ContentType(
    GUID = "{E6F693A7-1436-49CC-B7F0-D7555D530DD6}",
    GroupName = Globals.GroupNames.Specialized)]
public class StartPage : StartPageBase
{
    #region Content tab

    [CultureSpecific]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 100)]
    [Required]
    public virtual string Heading { get; set; }

    [CultureSpecific]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 110)]
    [UIHint(UIHint.Textarea, PresentationLayer.Edit)]
    public virtual string LeadText { get; set; }

    [CultureSpecific]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 120)]
    [AllowedTypes([
        typeof(TeaserBlock)
    ])]
    public virtual ContentArea MainContentArea { get; set; }

    [CultureSpecific]
    [Display(
        GroupName = SystemTabNames.Content,
        Order = 130)]
    public virtual ContentArea AnotherMainContentArea { get; set; }

    #endregion

    #region Public properties

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);
        //SiteName = "DEMO";
    }

    #endregion
}
