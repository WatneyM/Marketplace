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

        public void OnGet(string attribute)
        {
            if (attribute is not null)
                Attribute = _aService.GetAttribute(attribute);

            Groups = _gService.GetGroups().ToList();
        }

        public IActionResult OnGetDrop(string attribute)
        {
            _aService.DropAttribute(attribute);

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