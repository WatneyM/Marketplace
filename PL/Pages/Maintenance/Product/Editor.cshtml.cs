using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Attribute;
using DSL.Adapters.Maintenance.Category;
using DSL.Adapters.Maintenance.Group;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance.Product
{
    [Authorize(Roles = "Administrator")]
    [BindProperties]
    public class EditorModel(IProductService pService,
        ICategoryService cService,
        IAttributeGroupService gService,
        ISupplyService sService) : PageModel
    {
        private readonly IProductService _pService = pService;
        private readonly ICategoryService _cService = cService;
        private readonly IAttributeGroupService _gService = gService;
        private readonly ISupplyService _sService = sService;

        public List<CategoryRAdapter> Categories { get; set; } = [];
        public List<AttributeGroupXRAdapter> Groups { get; set; } = [];
        public ProductRWAdapter Product { get; set; } = new();
        [BindNever]
        public CategoryRWAdapter CurrentCategory { get; set; } = new();

        public bool KeyChecked { get; set; } = true;

        public void OnGet(string ckey, string pkey)
        {
            if (pkey is null)
            {
                Groups.AddRange(_gService.GetGroupsByCategory(ckey));
                CurrentCategory = _cService.GetCategory(ckey);

                Extend();
            }
            else if (_pService.KeyCheck(pkey))
            {
                Groups.AddRange(_gService.GetGroupsByCategory(ckey));
                Product = _pService.GetProduct(pkey);

                CurrentCategory = _cService.GetCategory(ckey);

                ExtendWithKey();
            }
            else
            {
                KeyChecked = !KeyChecked;
            }
        }

        public IActionResult OnGetDrop(string ckey, string pkey)
        {
            _pService.DropProduct(pkey);

            return Redirect("/maintenance/products/" + ckey);
        }

        public IActionResult OnPostPush(string ckey)
        {
            if (ModelState.IsValid)
            {
                SupplyWAdapter adapter = new()
                {
                    Key = Guid.NewGuid().ToString(),
                    Amount = Product.Amount,
                    OfCategory = Product.AttachedToCategory,
                    AttachedProduct = Product.Key
                };

                _pService.PushOrModifyProduct(Product);
                _sService.CreateAmount(adapter);
            }

            return Redirect("/maintenance/products/" + ckey);
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
                foreach (ProductAttributeXRWAdapter attribute in group.AttachedAttributes)
                {
                    if (!Product.AttachedValues
                        .Any(p => p.AttachedToAttribute == attribute.Key))
                    {
                        Product.AttachedValues
                            .Add(new() { AttachedToAttribute = attribute.Key });
                    }
                }
            }
        }
    }
}