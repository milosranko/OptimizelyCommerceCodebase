using EPiServer.Commerce.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Optimizely.Commerce.Demo.Infrastructure.Initialization;

[InitializableModule]
[ModuleDependency(typeof(EPiServer.Commerce.Initialization.InitializationModule))]
public class SiteInitialization : IInitializableModule
{
    public void Initialize(InitializationEngine context)
    {
        CatalogRouteHelper.MapDefaultHierarchialRouter(false);

        var catalogRoot = context.Locate.Advanced.GetRequiredService<EnableCatalogRoot>();
        catalogRoot.SetCatalogAccessRights();
    }

    public void Uninitialize(InitializationEngine context)
    { }
}
