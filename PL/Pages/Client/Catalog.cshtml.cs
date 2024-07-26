using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Client
{
    public class CatalogModel(ICategoryService service) : PageModel
    {
        private readonly ICategoryService _service = service;

        public List<CategoryRAdapter> Categories { get; set; } = [];

        public void OnGet()
        {
            Categories.AddRange(_service.GetCategoriesAsList());
        }
    }
}