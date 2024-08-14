using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Attribute;
using DSL.Adapters.Category;
using DSL.Adapters.Group;
using DSL.Adapters.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Product
{
    [BindProperties]
    public class EditorModel(IProductService pService,
        ICategoryService cService,
        IAttributeGroupService gService) : PageModel
    {
        private readonly IProductService _pService = pService;
        private readonly ICategoryService _cService = cService;
        private readonly IAttributeGroupService _gService = gService;

        public List<CategoryRAdapter> Categories { get; set; } = [];
        public List<AttributeGroupXRAdapter> Groups { get; set; } = [];
        public ProductRWAdapter Product { get; set; } = new();
        [BindNever]
        public CategoryRWAdapter CurrentCategory { get; set; } = new();

        public void OnGet(string cid, string pid)
        {
            Groups.AddRange(_gService.GetGroupsByCategory(cid));
            CurrentCategory = _cService.GetCategory(cid);

            Extend();

            if (pid is not null)
            {
                Product = _pService.GetProduct(pid);

                ExtendWithKey();
            }
        }

        public IActionResult OnGetDrop(string cid, string pid)
        {
            _pService.DropProduct(pid);

            return Redirect("/maintenance/products?cid=" + cid);
        }

        public IActionResult OnPostPush(string cid)
        {
            if (ModelState.IsValid)
                _pService.PushOrModifyProduct(Product);

            return Redirect("/maintenance/products?cid=" + cid);
        }

        public void Extend()
        {
            foreach (AttributeGroupXRAdapter group in Groups)
            {
                foreach (ProductAttributeXRWAdapter attribute in group.AttachedAttributes)
                {
                    Product.AttachedValues.Add(new());
                }
            }
        }

        public void ExtendWithKey()
        {
            foreach (AttributeGroupXRAdapter group in Groups)
            {
                foreach (ProductAttributeXRWAdapter attr in group.AttachedAttributes)
                {
                    if (!Product.AttachedValues
                        .Any(p => p.AttachedToAttribute == attr.Key))
                    {
                        Product.AttachedValues
                            .Add(new() { AttachedToAttribute = attr.Key });
                    }
                }
            }
        }
    }
}