using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Product;
using DSL.Adapters.Maintenance.Category;
using DSL.Services.Declarations;
using DSL.Adapters.Maintenance.Order;
using Microsoft.AspNetCore.Http.Extensions;

namespace PL.Pages.Client
{
    [BindProperties]
    public class ListModel(ISessionService session,
        IProductService pService,
        ICategoryService cService) : PageModel
    {
        private readonly ISessionService _session = session;
        private readonly IProductService _pService = pService;
        private readonly ICategoryService _cService = cService;

        public List<ProductRAdapter> Products { get; set; } = [];
        public CategoryRWAdapter Category { get; set; } = new ();

        public void OnGet(string key)
        {
            Products.AddRange(_pService.GetProductsOfCategory(key));
            Category = _cService.GetCategory(key);
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
    }
}