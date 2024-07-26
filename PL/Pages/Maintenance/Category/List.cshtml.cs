using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Category
{
    [BindProperties]
    public class ListModel(ICategoryService service) : PageModel
    {
        private readonly ICategoryService _service = service;

        public List<CategoryRAdapter> Categories { get; set; } = [];

        public void OnGet()
        {
            Categories = _service.GetCategoriesAsList().ToList();
        }
    }
}