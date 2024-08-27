using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Category;
using DSL.Adapters.Maintenance.Group;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Group
{
    [Authorize(Roles = "Administrator")]
    [BindProperties]
    public class EditorModel(IAttributeGroupService gService,
        ICategoryService cService) : PageModel
    {
        private readonly IAttributeGroupService _gService = gService;
        private readonly ICategoryService _cService = cService;

        public AttributeGroupRWAdapter Group { get; set; } = new();
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public bool KeyChecked { get; set; } = true;

        public void OnGet(string key)
        {
            if (key is null)
            {
                Categories.AddRange(_cService.GetAttachableCategories());
            }
            else if (_gService.KeyCheck(key))
            {
                Group = _gService.GetGroup(key);
                Categories.AddRange(_cService.GetAttachableCategories());
            }
            else
            {
                KeyChecked = !KeyChecked;
            }
        }

        public IActionResult OnGetDrop(string key)
        {
            _gService.DropGroup(key);

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