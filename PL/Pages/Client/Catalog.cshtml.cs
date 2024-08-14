using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Product;
using DSL.Services.Declarations;
using DSL.Adapters.Category;

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

        public void OnGet(string cid)
        {
            Products.AddRange(_pService.GetProductsOfCategory(cid));
            Category = _cService.GetCategory(cid);
        }
    }
}