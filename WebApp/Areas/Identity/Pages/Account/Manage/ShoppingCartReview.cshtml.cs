using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class ShoppingCartReviewModel : PageModel
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public SignInManager<ApplicationUser> SignInManager { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public IDataAcces DataAcces { get; set; }
        private readonly List<Item> items;
        public ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartReviewModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            IDataAcces dataAcces
            )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            items = new List<Item>();
            DataAcces = dataAcces;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ApplicationUser = await UserManager.GetUserAsync(User);

            if (ApplicationUser != null)
            {
                List<string> itemsString = ApplicationUser.GetItems();
                //check if there is anything in the cart
                if (itemsString != null && itemsString.Count != 0)
                {
                    ShoppingCart = new ShoppingCart(itemsString, DataAcces);
                }
            }
            return Page();
        }

        public async Task<JsonResult> OnPostAsync(string itemId)
        {
            ApplicationUser = await UserManager.GetUserAsync(User);
            Item item = DataAcces.GetItem(itemId);
            ApplicationUser.Items = ApplicationUser.EraseItem(item);
            IdentityResult identityResult = await UserManager.UpdateAsync(ApplicationUser);
            if (identityResult.Succeeded)
            {
                return new JsonResult(item.Price);
            }
            else return null;
        }
    }
}