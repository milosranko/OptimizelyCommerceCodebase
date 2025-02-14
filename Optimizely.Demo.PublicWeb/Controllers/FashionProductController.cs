using Microsoft.AspNetCore.Mvc;
using Optimizely.Demo.Commerce.Core.Controllers;
using Optimizely.Demo.Commerce.Models.Products;
using Optimizely.Demo.Commerce.Models.ViewModels;

namespace Optimizely.Demo.PublicWeb.Controllers;

public class FashionProductController : CommerceControllerBase<FashionProduct>
{
    public IActionResult Index(FashionProduct currentPage)
    {
        var model = new ProductViewModel<FashionProduct>(currentPage);

        return View(model);
    }
}
