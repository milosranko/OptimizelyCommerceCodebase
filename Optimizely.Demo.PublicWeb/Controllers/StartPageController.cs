using Microsoft.AspNetCore.Mvc;
using Optimizely.Commerce.Demo.Models.Cms;
using Optimizely.Demo.Core.Controllers;
using Optimizely.Demo.Core.Models.ViewModels;

namespace Optimizely.Demo.PublicWeb.Controllers;

public class StartPageController : PageControllerBase<StartPage>
{
    public IActionResult Index(StartPage currentPage)
    {
        var model = PageViewModel.Create(currentPage);

        return View(model);
    }
}
