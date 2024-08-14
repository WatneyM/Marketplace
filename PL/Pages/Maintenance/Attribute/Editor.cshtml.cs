using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Attribute;
using DSL.Adapters.Group;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Attribute
{
    [BindProperties]
    public class EditorModel(IAttributeService aService,
        IAttributeGroupService gService) : PageModel
    {
        private readonly IAttributeService _aService = aService;
        private readonly IAttributeGroupService _gService = gService;

        public AttributeRWAdapter Attribute { get; set; } = new();
        public List<AttributeGroupRWAdapter> Groups { get; set; } = [];

        public void OnGet(string aid)
        {
            if (aid is not null)
                Attribute = _aService.GetAttribute(aid);

            Groups.AddRange(_gService.GetGroups());
        }

        public IActionResult OnGetDrop(string aid)
        {
            _aService.DropAttribute(aid);

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