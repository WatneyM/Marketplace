using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Attribute;
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

        public void OnGet()
        {
            Attributes = _aService.GetAttributesAsList().ToList();
        }
    }
}