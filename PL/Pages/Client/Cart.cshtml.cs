using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Order;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;
using Microsoft.AspNetCore.Authorization;

namespace PL.Pages.Client
{
    [Authorize(Roles = "Customer, Administrator")]
    [BindProperties]
    public class CartModel(ISessionService session,
        IProductService pService,
        IOrderService oService,
        ISupplyService sService) : PageModel
    {
        private readonly ISessionService _session = session;
        private readonly IProductService _pService = pService;
        private readonly IOrderService _oService = oService;
        private readonly ISupplyService _sService = sService;

        public OrderWAdapter Order { get; set; } = new();
        public List<ProductRAdapter> Products { get; set; } = [];
        public SupplementaryOA Info { get; set; } = new();

        public void OnGet()
        {
            List<OrderWAdapter>? infos = _session.Get<List<OrderWAdapter>>(HttpContext.Session, "preorder");

            if (infos is not null)
            {
                foreach (OrderWAdapter info in infos)
                {
                    Products.Add(_pService.GetProductInfo(info.AttachedProduct!));
                }
            }
        }

        public IActionResult OnPostCreate()
        {
            List<OrderWAdapter>? infos = _session.Get<List<OrderWAdapter>>(HttpContext.Session, "preorder")!;
            OrderWAdapter info = infos.Find(p => p.AttachedProduct == Info.AttachedProduct)!;

            info.Address = Info.Address;
            info.Amount = Info.Amount;

            if (_sService.PreserveAmount(info.AttachedProduct!, Info.Amount))
            {
                _oService.MakeOrder(info);

                infos.Remove(info);
                HttpContext.Session.Remove("preorder");
                _session.Set(HttpContext.Session, "preorder", infos);
            }

            return Redirect("/cart");
        }

        public IActionResult OnGetDecline(string key)
        {
            List<OrderWAdapter> infos = _session.Get<List<OrderWAdapter>>(HttpContext.Session, "preorder")!;
            OrderWAdapter info = infos.Find(p => p.AttachedProduct == key)!;

            infos.Remove(info);

            HttpContext.Session.Remove("preorder");
            _session.Set(HttpContext.Session, "preorder", infos);

            return Redirect("/cart");
        }
    }
}