using Optimizely.Demo.Commerce.Models.Products.Base;

namespace Optimizely.Demo.Commerce.Models.ViewModels;

public sealed class ProductViewModel<T> : IProductViewModel<T> where T : ProductBase //: PageViewModel<T> 
{
    public ProductViewModel(T currentPage)// : base(currentPage)
    {
        CurrentPage = currentPage;
        Quantity = 1;
        Code = currentPage.Code;
        //ListPrice = currentPage.GetListPrice();
    }

    public IEnumerable<VariantsViewModel> VariantsModel { get; set; }
    public IEnumerable<VariantsViewModel> RelatedProducts { get; set; }
    public string Code { get; set; }
    public int Quantity { get; set; }
    public string BannerText { get; set; }
    public T CurrentPage { get; set; }

    //public List<VariantDropdownInfo> AddToCartDropdown { get; set; }
}
