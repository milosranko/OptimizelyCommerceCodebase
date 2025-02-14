using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Web.Mvc;

namespace Optimizely.Demo.Commerce.Core.Controllers;

public class CommerceControllerBase<T> : ContentController<T> where T : CatalogContentBase
{
    public T CatalogContent { get; set; }
}
