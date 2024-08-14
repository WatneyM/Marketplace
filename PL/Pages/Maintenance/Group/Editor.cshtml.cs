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
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public void OnGet(string gid)
        {
            if (gid is not null)
                Group = _gService.GetGroup(gid);

            Categories.AddRange(_cService.GetAttachableCategories());
        }

        public IActionResult OnGetDrop(string gid)
        {
            _gService.DropGroup(gid);

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