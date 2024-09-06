using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Group;
using DSL.Adapters.Maintenance.Order;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Client
{
    [BindProperties]
    public class ViewModel(ISessionService session,
        IProductService pService,
        IAttributeGroupService gService,
        ISupplyService sService) : PageModel
    {
        private readonly ISessionService _session = session;
        private readonly IProductService _pService = pService;
        private readonly IAttributeGroupService _gService = gService;
        private readonly ISupplyService _sService = sService;

        public ProductRWAdapter Product = new();
        public List<AttributeGroupXRAdapter> Groups { get; set; } = [];
        public int Amount { get; set; }

        public void OnGet(string key)
        {
            Product = _pService.GetProduct(key);
            Groups.AddRange(_gService.GetGroupsByCategory(Product.AttachedToCategory));
            Amount = _sService.GetSupplyRecord(key).Amount;

            List<OrderWAdapter>? infos = _session.Get<List<OrderWAdapter>>(HttpContext.Session, "preorder");

            if (infos is null)
            {
                ViewData["AllowOrdering"] = true;
                return;
            }

            OrderWAdapter? info = infos!.Find(p => p.AttachedProduct == Product.Key);

            if (info is null) ViewData["AllowOrdering"] = true;
            else ViewData["AllowOrdering"] = info!.AttachedUsername != User.Identity!.Name;
        }

        public IActionResult OnGetPush(string key)
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
            else return Redirect($"/authentication?ReturnUrl=%2Fproduct%2F{key}");

            return Redirect($"/product/{key}");
        }
    }
}