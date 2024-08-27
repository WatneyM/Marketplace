using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Category;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Category
{
    [Authorize(Roles = "Administrator")]
    [BindProperties]
    public class ListModel(ICategoryService service) : PageModel
    {
        private readonly ICategoryService _service = service;

        public List<CategoryRAdapter> Primaries { get; set; } = [];
        public List<CategoryRAdapter> Attached { get; set; } = [];

        public void OnGet()
        {
            Primaries.AddRange(_service.GetPrimaryCategories());
            Attached.AddRange(_service.GetAttachableCategories());
        }
    }
}