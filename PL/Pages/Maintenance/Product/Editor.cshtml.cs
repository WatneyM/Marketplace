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

        public ProductRWAdapter Product { get; set; } = new();
        public List<CategoryRWAdapter> Categories { get; set; } = [];
        public List<AttributeGroupXRAdapter> Groups { get; set; } = [];
        [BindNever]
        public string? CurrentCategory { get; set; }

        public void OnGet(string category, string product)
        {
            if (category is null)
                Categories = _cService.GetAttachableCategories().ToList();
            else
            {
                Groups = _gService.GetGroupsByCategory(category).ToList();
                CurrentCategory = category;

                Extend();

                if (product is not null)
                {
                    Product = _pService.GetProduct(product);

                    ExtendWithKey();
                }
            }
        }

        public IActionResult OnGetDrop(string product)
        {
            _pService.DropProduct(product);

            return Redirect("/maintenance/products");
        }

        public IActionResult OnPostPush()
        {
            if (ModelState.IsValid)
                _pService.PushOrModifyProduct(Product);

            return Redirect("/maintenance/products");
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