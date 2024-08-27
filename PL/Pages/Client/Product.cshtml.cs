using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Group;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Client
{
    [BindProperties]
    public class ViewModel(IProductService pService,
        IAttributeGroupService gService) : PageModel
    {
        private readonly IProductService _pService = pService;
        private readonly IAttributeGroupService _gService = gService;

        public ProductRWAdapter Product = new();
        public List<AttributeGroupXRAdapter> Groups { get; set; } = [];

        public void OnGet(string key)
        {
            Product = _pService.GetProduct(key);
            Groups.AddRange(_gService.GetGroupsByCategory(Product.AttachedToCategory));
        }
    }
}
