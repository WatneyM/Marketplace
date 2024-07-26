using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Category
{
    [BindProperties]
    public class EditorModel(ICategoryService service) : PageModel
    {
        private readonly ICategoryService _service = service;

        public CategoryRWAdapter Category { get; set; } = new();
        public List<CategoryRWAdapter> Categories { get; set; } = [];

        public void OnGet(string category)
        {
            if (category is not null)
            {
                Category = _service.GetCategory(category);
                Categories = _service.GetCategoriesExceptKey(category).ToList();
            }
            else Categories = _service.GetCategories().ToList();
        }

        public IActionResult OnGetDrop(string category)
        {
            _service.DropCategory(category);

            return Redirect("/maintenance/categories");
        }

        public IActionResult OnPostPush()
        {
            if (ModelState.IsValid)
                _service.PushOrModifyCategory(Category);

            return Redirect("/maintenance/categories");
        }
    }
}