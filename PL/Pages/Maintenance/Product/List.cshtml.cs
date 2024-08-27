using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Category;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Product
{
    [Authorize(Roles = "Administrator")]
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

        public void OnGet(string key)
        {
            if (key is not null)
            {
                Products.AddRange(_pService.GetProductsOfCategory(key));

                CurrentCategory = key;
            }

            ProductsCount = _pService.GetCount();
            Categories.AddRange(_cService.GetAttachableCategories());
        }
    }
}