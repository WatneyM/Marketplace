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
    public class ListModel(IAttributeService aService,
        IAttributeGroupService gService,
        ICategoryService cService) : PageModel
    {
        private readonly IAttributeService _aService = aService;
        private readonly IAttributeGroupService _gService = gService;
        private readonly ICategoryService _cService = cService;

        public List<AttributeRAdapter> Attributes { get; set; } = [];
        public List<AttributeGroupRAdapter> Groups { get; set; } = [];
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public void OnGet()
        {
            Attributes.AddRange(_aService.GetAttributesAsList());
            Groups.AddRange(_gService.GetGroupsAsList());
            Categories.AddRange(_cService.GetAttachableCategories());
        }
    }
}