using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using DSL.Adapters.Maintenance.Order;
using DSL.Adapters.Maintenance.Product;
using DSL.Services.Declarations;

namespace PL.Pages.Client
{
    [Authorize(Roles = "Customer, Administrator")]
    [BindProperties]
    public class OrdersModel(IOrderService orderService,
        IProductService productService,
        ISupplyService supplyService,
        IAuthService authService) : PageModel
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IProductService _productService = productService;
        private readonly ISupplyService _supplyService = supplyService;
        private readonly IAuthService _authService = authService;

        public List<OrderRAdapter> Orders { get; set; } = [];
        public List<ShortOrderAdapter> Products { get; set; } = [];

        public async Task OnGetAsync()
        {
            ViewData["Holder"] = await _authService.GetFullname(User.Identity!.Name!);

            Orders.AddRange(_orderService.GetOrders(User.Identity!.Name!));
            Products.AddRange(_productService.GetProducts(Orders.Select(s => s.AttachedProduct)!));
        }

        public IActionResult OnPostDecline(string key)
        {
            OrderRAdapter order = _orderService.GetOrder(key);

            _orderService.RemoveOrder(key);
            _supplyService.RestoreAmount(order.AttachedProduct, order.Amount);

            return Redirect("/orders");
        }
    }
}