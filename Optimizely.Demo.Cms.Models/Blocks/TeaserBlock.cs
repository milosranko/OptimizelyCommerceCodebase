using EPiServer.Authorization;
using EPiServer.Cms.Shell.UI.ObjectEditing;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using Optimizely.Demo.ContentTypes.Attributes;
using Optimizely.Demo.ContentTypes.Models.Blocks.Base;
using Optimizely.Demo.Core.Models.Blocks.Local;
using System.ComponentModel.DataAnnotations;

namespace Optimizely.Demo.ContentTypes.Blocks;

[Access(Roles = $"{Roles.WebAdmins}")]
[ContentType(GUID = "{C98C99EA-A630-49CD-8A45-5AEF47EE265D}")]
[SiteImageUrl("TeaserBlock.png")]
[InlineBlockEditSettings(ShowNameProperty = true)]
public class TeaserBlock : BlockBase
{
	#region Content tab

	[ReloadOnChange]
	[CultureSpecific]
	[Display(
		GroupName = SystemTabNames.Content,
		Order = 100)]
	[StringLength(50)]
	public virtual string Heading { get; set; }

	[CultureSpecific]
	[Display(
		GroupName = SystemTabNames.Content,
		Order = 110)]
	[UIHint(UIHint.Textarea, PresentationLayer.Edit)]
	public virtual string LeadText { get; set; }

	[Display(
		GroupName = SystemTabNames.Content,
		Order = 120)]
	public virtual LinkBlock LinkButton { get; set; }

	[CultureSpecific]
	[Display(
		GroupName = SystemTabNames.Content,
		Order = 130)]
	[UIHint(UIHint.Image)]
	public virtual ContentReference Image { get; set; }

	#endregion
}
