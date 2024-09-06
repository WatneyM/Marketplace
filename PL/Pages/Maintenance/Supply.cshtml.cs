using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Category;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Maintenance
{
    [BindProperties]
    public class SupplyModel(ICategoryService cService,
        IProductService pService,
        ISupplyService sService) : PageModel
    {
        private readonly ICategoryService _cService = cService;
        private readonly IProductService _pService = pService;
        private readonly ISupplyService _sService = sService;

        public List<CategoryRAdapter> Categories { get; set; } = [];
        public List<SupplyRAdapter> SupplyList { get; set; } = [];

        public void OnGet(string key)
        {
            if (_cService.KeyCheck(key))
            {
                SupplyList.AddRange(_sService.GetSupplyList(key));

                IEnumerable<KeyValuePair<string, string>> prods
                    = _pService.GetProductNames(SupplyList.Select(s => s.AttachedProduct));

                SupplyList.ForEach(a
                    => a.AttachedProduct = prods.Single(p => p.Key == a.AttachedProduct).Value);
            }

            Categories.AddRange(_cService.GetAttachableCategories());
        }

        public IActionResult OnPostUpdateSupplyList(string key)
        {
            _sService.SetAmount(SupplyList.AsEnumerable());

            return Redirect("/maintenance/supply/" + key);
        }
    }
}
