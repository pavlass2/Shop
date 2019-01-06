using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class MyOrderModel : PageModel
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly IDataAcces dataAcces;
        public ShoppingCart ShoppingCart { get; set; }
        public Order Order { get; set; }

        public MyOrderModel(UserManager<ApplicationUser> userManager, IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Order = dataAcces.GetOrder(int.Parse(id));
            ApplicationUser applicationUser = await userManager.GetUserAsync(User);

            if (Order.CustomerId == applicationUser.Id)
            {
                ShoppingCart = new ShoppingCart(Order.Items.Split(";").ToList(), dataAcces);
                return Page();
            }
            else return Forbid();
        }
    }
}