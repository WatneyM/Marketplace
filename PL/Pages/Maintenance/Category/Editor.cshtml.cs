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
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public void OnGet(string cid)
        {
            if (cid is not null)
            {
                Category = _service.GetCategory(cid);
                Categories.AddRange(_service.GetPrimaryCategories(Category.Key));
            }
            else Categories.AddRange(_service.GetPrimaryCategories());
        }

        public IActionResult OnGetDrop(string cid)
        {
            _service.DropCategory(cid);

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