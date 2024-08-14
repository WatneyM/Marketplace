using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Group;
using DSL.Services.Declarations;
using DSL.Adapters.Category;

namespace PL.Pages.Maintenance.Group
{
    [BindProperties]
    public class ListModel(IAttributeGroupService gService,
        ICategoryService cService) : PageModel
    {
        private readonly IAttributeGroupService _gService = gService;
        private readonly ICategoryService _cService = cService;

        public List<AttributeGroupRAdapter> Groups { get; set; } = [];
        public List<CategoryRAdapter> Categories { get; set; } = [];

        public void OnGet()
        {
            Groups.AddRange(_gService.GetGroupsAsList());
            Categories.AddRange(_cService.GetAttachableCategories());
        }
    }
}