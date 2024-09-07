using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

using DSL.Adapters.Maintenance.Product;
using DSL.Adapters.Maintenance.Category;
using DSL.Services.Declarations;
using DSL.Adapters.Maintenance.Order;

namespace PL.Pages.Client
{
    [BindProperties]
    public class ListModel(ISessionService session,
        IProductService pService,
        ICategoryService cService,
        IAttributeService aService) : PageModel
    {
        private readonly ISessionService _session = session;
        private readonly IProductService _pService = pService;
        private readonly ICategoryService _cService = cService;
        private readonly IAttributeService _aService = aService;

        public List<KeyValuePair<string, string>> AttributeNames { get; set; } = [];
        public List<KeyValuePair<string, bool>> FilterStorage { get; set; } = [];
        public List<KeyValuePair<string, string>> AttributeValues { get; set; } = [];
        public List<string> AttributeKeys { get; set; } = [];
        public List<ProductRAdapter> Products { get; set; } = [];
        public CategoryRWAdapter Category { get; set; } = new ();

        public void OnGet(string key, string filter)
        {
            Category = _cService.GetCategory(key);

            if (_pService.GetCountOfCategory(key) == 0) return;
            AttributeNames.AddRange(_aService.GetFilteredAttributesNames(key));
            AttributeValues.AddRange(_aService
                .GetFiltersValues(_aService
                .GetFilteredAttributes(_pService
                .GetRelatedAttributeKeys(key))));

            foreach (var item in AttributeValues)
            {
                if (AttributeKeys.Contains(item.Key)) continue;
                else AttributeKeys.Add(item.Key);
            }

            foreach (var item in AttributeKeys)
            {
                foreach (var item2 in AttributeValues.Where(p => p.Key == item))
                    FilterStorage.Add(new KeyValuePair<string, bool>(item2.Value, false));
            }

            if (filter is null)
            {
                Products.AddRange(_pService.GetProductsOfCategory(key));
            }
            else
            {
                Dictionary<string, List<string>> container = [];

                foreach (var item in filter.Split("-"))
                    FilterStorage[FilterStorage.IndexOf(FilterStorage.Find(p => p.Key == item))] = new KeyValuePair<string, bool>(item, true);

                foreach (var item in AttributeKeys) container.Add(AttributeValues.Find(p => p.Key == item).Key, []);
                foreach (var item in FilterStorage)
                {
                    if (item.Value)
                    {
                        container[AttributeValues.Find(p => p.Value == item.Key).Key].Add(item.Key);
                    }
                }
                foreach (var item in container)
                {
                    if (item.Value.Count == 0) container.Remove(item.Key);
                }

                Products.AddRange(_pService.GetProductsOfCategoryWithFilter(key, container.Select(p => p.Value)));
            }
        }

        public IActionResult OnGetPush(string key, string ctg)
        {
            List<OrderWAdapter>? infos = _session.Get<List<OrderWAdapter>>(HttpContext.Session, "preorder");
            OrderWAdapter pre = new()
            {
                Key = Guid.NewGuid().ToString(),
                AttachedProduct = key,
                AttachedUsername = User.Identity!.Name
            };

            if (User.Identity.IsAuthenticated)
            {
                if (infos is null)
                {
                    infos = [pre];

                    _session.Set(HttpContext.Session, "preorder", infos);
                }
                else
                {
                    infos.Add(pre);

                    HttpContext.Session.Remove("preorder");
                    _session.Set(HttpContext.Session, "preorder", infos);
                }
            }
            else return Redirect("/authentication?ReturnUrl=%2Fcatalog%2F" + ctg);

            return Redirect("/catalog/" + ctg);
        }

        public IActionResult OnPostFilter()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendJoin("-", FilterStorage.Where(p => p.Value).Select(s => s.Key));

            return RedirectToPage("Catalog", new { key = RouteData.Values["key"],
                filter = builder.ToString() });
        }
    }
}