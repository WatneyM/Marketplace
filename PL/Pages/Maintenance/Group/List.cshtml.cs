using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Group;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Group
{
    [BindProperties]
    public class ListModel(IAttributeGroupService service) : PageModel
    {
        private readonly IAttributeGroupService _service = service;

        public List<AttributeGroupRAdapter> Groups { get; set; } = [];

        public void OnGet()
        {
            Groups = _service.GetGroupsAsList().ToList();
        }
    }
}