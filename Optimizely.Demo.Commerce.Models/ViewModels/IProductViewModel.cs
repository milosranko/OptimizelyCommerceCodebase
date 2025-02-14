using Optimizely.Demo.Commerce.Models.Products.Base;

namespace Optimizely.Demo.Commerce.Models.ViewModels;

public interface IProductViewModel<out T> where T : ProductBase
{
    T CurrentPage { get; }
}
