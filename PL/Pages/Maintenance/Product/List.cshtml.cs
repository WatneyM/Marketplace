using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Product;
using DSL.Services.Declarations;
using DSL.Adapters.Category;

namespace PL.Pages.Maintenance.Product
{
    [BindProperties]
    public class ListModel(IProductService pService,
        ICategoryService cService) : PageModel
    {
        private readonly IProductService _pService = pService;
        private readonly ICategoryService _cService = cService;

        public List<ProductRAdapter> Products { get; set; } = [];
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public string? CurrentCategory { get; set; }
        public int ProductsCount { get; set; }

        public void OnGet(string cid)
        {
            if (cid is not null)
            {
                Products.AddRange(_pService.GetProductsOfCategory(cid));

                CurrentCategory = cid;
            }

            ProductsCount = _pService.GetCount();
            Categories.AddRange(_cService.GetAttachableCategories());
        }
    }
}