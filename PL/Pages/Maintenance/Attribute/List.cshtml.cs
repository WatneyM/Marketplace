using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Attribute;
using DSL.Adapters.Group;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Attribute
{
    [BindProperties]
    public class ListModel(IAttributeService aService,
        IAttributeGroupService gService) : PageModel
    {
        private readonly IAttributeService _aService = aService;
        private readonly IAttributeGroupService _gService = gService;

        public List<AttributeRAdapter> Attributes { get; set; } = [];
        public List<AttributeGroupRAdapter> Groups { get; set; } = [];

        public void OnGet()
        {
            Attributes.AddRange(_aService.GetAttributesAsList());
            Groups.AddRange(_gService.GetGroupsAsList());
        }
    }
}