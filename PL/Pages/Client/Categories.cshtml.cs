using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Client
{
    public class CatalogModel(ICategoryService cService,
        IProductService pService) : PageModel
    {
        private readonly ICategoryService _cService = cService;
        private readonly IProductService _pService = pService;

        public List<CategoryRAdapter> Primaries { get; set; } = [];
        public List<CategoryRAdapter> Attached { get; set; } = [];

        public void OnGet()
        {
            Primaries.AddRange(_cService.GetPrimaryCategories());
            Attached.AddRange(_cService.GetAttachableCategories());

            foreach (CategoryRAdapter category in Attached)
                category.AttachedProducts = _pService.GetCountOfCategory(category.Key);
        }
    }
}