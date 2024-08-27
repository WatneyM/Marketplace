using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Category
{
    [Authorize(Roles = "Administrator")]
    [BindProperties]
    public class EditorModel(ICategoryService service) : PageModel
    {
        private readonly ICategoryService _service = service;

        public CategoryRWAdapter Category { get; set; } = new();
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public bool KeyChecked { get; set; } = true;

        public void OnGet(string key)
        {
            if (key is null)
            {
                Categories.AddRange(_service.GetPrimaryCategories());
            }
            else if (_service.KeyCheck(key))
            {
                Category = _service.GetCategory(key);
                Categories.AddRange(_service.GetPrimaryCategories(Category.Key));
            }
            else
            {
                KeyChecked = !KeyChecked;
            }
        }

        public IActionResult OnGetDrop(string key)
        {
            _service.DropCategory(key);

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