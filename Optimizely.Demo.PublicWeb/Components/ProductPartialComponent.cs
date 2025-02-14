using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Optimizely.Demo.Commerce.Models.Products.Base;

namespace Optimizely.Demo.PublicWeb.Components;

[TemplateDescriptor(Inherited = true, TemplateTypeCategory = TemplateTypeCategories.MvcPartialComponent)]
public class ProductPartialComponent : AsyncPartialContentComponent<ProductBase>
{
    protected override async Task<IViewComponentResult> InvokeComponentAsync(ProductBase currentContent)
    {
        return await Task.FromResult(View("_Product", currentContent));
    }
}
