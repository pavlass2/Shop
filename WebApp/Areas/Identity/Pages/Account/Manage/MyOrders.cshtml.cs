using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class MyOrdersModel : PageModel
    {
        readonly IDataAcces dataAcces;
        readonly UserManager<ApplicationUser> userManager;
        public IQueryable<Order> Orders { get; private set; }

        public MyOrdersModel(IDataAcces dataAcces, UserManager<ApplicationUser> userManager)
        {
            this.dataAcces = dataAcces;
            this.userManager = userManager;
        }

        public async Task OnGet()
        {
            ApplicationUser user = await userManager.GetUserAsync(User);
            Orders = dataAcces.GetOrdersOfUser(user.Id);
        }
    }
}