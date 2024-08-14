using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Group;
using DSL.Adapters.Product;
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

        public void OnGet(string pid)
        {
            Product = _pService.GetProduct(pid);
            Groups.AddRange(_gService.GetGroupsByCategory(Product.AttachedToCategory));
        }
    }
}
