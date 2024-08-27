using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Attribute;
using DSL.Adapters.Maintenance.Category;
using DSL.Adapters.Maintenance.Group;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Attribute
{
    [Authorize(Roles = "Administrator")]
    [BindProperties]
    public class EditorModel(IAttributeService aService,
        IAttributeGroupService gService,
        ICategoryService cService) : PageModel
    {
        private readonly IAttributeService _aService = aService;
        private readonly IAttributeGroupService _gService = gService;
        private readonly ICategoryService _cService = cService;

        public AttributeRWAdapter Attribute { get; set; } = new();
        public List<AttributeGroupRWAdapter> Groups { get; set; } = [];
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public bool KeyChecked { get; set; } = true;

        public void OnGet(string key)
        {
            if (key is null)
            {
                Groups.AddRange(_gService.GetGroups());
                Categories.AddRange(_cService.GetAttachableCategories());
            }
            else if (_aService.KeyCheck(key))
            {
                Attribute = _aService.GetAttribute(key);
                Groups.AddRange(_gService.GetGroups());
                Categories.AddRange(_cService.GetAttachableCategories());
            }
            else
            {
                KeyChecked = !KeyChecked;
            }
        }

        public IActionResult OnGetDrop(string key)
        {
            _aService.DropAttribute(key);

            return Redirect("/maintenance/attributes");
        }

        public IActionResult OnPostPush()
        {
            if (ModelState.IsValid)
                _aService.PushOrModifyAttribute(Attribute);

            return Redirect("/maintenance/attributes");
        }
    }
}