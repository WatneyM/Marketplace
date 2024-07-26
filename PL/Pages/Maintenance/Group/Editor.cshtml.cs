using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Category;
using DSL.Adapters.Group;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Group
{
    [BindProperties]
    public class EditorModel(IAttributeGroupService gService,
        ICategoryService cService) : PageModel
    {
        private readonly IAttributeGroupService _gService = gService;
        private readonly ICategoryService _cService = cService;

        public AttributeGroupRWAdapter Group { get; set; } = new();
        public List<CategoryRWAdapter> Categories { get; set; } = [];

        public void OnGet(string group)
        {
            if (group is not null)
                Group = _gService.GetGroup(group);

            Categories = _cService.GetAttachableCategories().ToList();
        }

        public IActionResult OnGetDrop(string group)
        {
            _gService.DropGroup(group);

            return Redirect("/maintenance/groups");
        }

        public IActionResult OnPostPush()
        {
            if (ModelState.IsValid)
                _gService.PushOrModifyGroup(Group);

            return Redirect("/Maintenance/groups");
        }
    }
}