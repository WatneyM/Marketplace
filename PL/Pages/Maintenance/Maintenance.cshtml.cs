using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PL.Pages.Maintenance
{
    [Authorize(Roles = "Administrator")]
    public class MaintenanceModel : PageModel
    {
    }
}