using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Product;
using DSL.Adapters.Maintenance.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Client
{
    [BindProperties]
    public class ListModel(IProductService pService,
        ICategoryService cService) : PageModel
    {
        private readonly IProductService _pService = pService;
        private readonly ICategoryService _cService = cService;

        public List<ProductRAdapter> Products { get; set; } = [];
        public CategoryRWAdapter Category { get; set; } = new ();

        public void OnGet(string key)
        {
            Products.AddRange(_pService.GetProductsOfCategory(key));
            Category = _cService.GetCategory(key);
        }
    }
}