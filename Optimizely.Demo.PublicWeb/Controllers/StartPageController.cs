using Microsoft.AspNetCore.Mvc;
using Optimizely.Commerce.Demo.Models.Cms;
using Optimizely.Demo.Cms.Core.Models.ViewModels;
using Optimizely.Demo.Core.Controllers;

namespace Optimizely.Demo.PublicWeb.Controllers;

public class StartPageController : PageControllerBase<StartPage>
{
    public IActionResult Index(StartPage currentPage)
    {
        var model = PageViewModel.Create(currentPage);

        return View(model);
    }
}
