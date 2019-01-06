using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class OrderSummaryModel : PageModel
    {
        public IDataAcces DataAcces { get; set; }
        public UserManager<ApplicationUser> UserManager { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Order Order { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public OrderSummaryModel(IDataAcces dataAcces, UserManager<ApplicationUser> userManager)
        {
            DataAcces = dataAcces;
            UserManager = userManager;            
        }
        public async Task OnGetAsync()
        {
            ApplicationUser = await UserManager.GetUserAsync(User);
            Order = DataAcces.GetLastOrder(ApplicationUser.Id);
            ShoppingCart = new ShoppingCart(Order.Items.Split(";").ToList(), DataAcces);
            
        }
    }
}