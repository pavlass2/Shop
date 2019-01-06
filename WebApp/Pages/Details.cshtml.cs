using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IDataAcces dataAcces;
        //private readonly IUserAcces userAcces;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        public Item Item { get; set; }
        public int SameItemAlreadyInShoppingCart;
        public string Language { get; set; }
        public string Category { get; set; }

        public DetailsModel(
            IDataAcces dataAcces,
            //IUserAcces userAcces,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.dataAcces = dataAcces;
            //this.userAcces = userAcces;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Item = dataAcces.GetItem(id);
            if (signInManager.IsSignedIn(User))
            {
                ApplicationUser user = await userManager.GetUserAsync(User);
               
                SameItemAlreadyInShoppingCart = user.IsInShoppingCart(Item.Id);
            }
            if (Item.Language == Lang.cze)
            {
                Language = "Czech";
            }
            else if (Item.Language == Lang.eng)
            {
                Language = "English";
            }
            else
            {
                Language = "German";
            }

            if (Item.Category == Models.Category.Classic)
            {
                Category = "Classic";
            }
            else if (Item.Category == Models.Category.Fantasy)
            {
                Category = "Fantasy";
            }
            else if (Item.Category == Models.Category.HistoryFiction)
            {
                Category = "History-fiction";
            }
            else if (Item.Category == Models.Category.ScienceFiction)
            {
                Category = "Science-fiction";
            }
            else
            {
                Category = "Workbook";
            }

            return Page();
        }

        public async Task<JsonResult> OnPost(string id, string amount)
        {
            int itemAmount = int.Parse(amount);
            Item = dataAcces.GetItem(id);
            ApplicationUser user = await userManager.GetUserAsync(User);

            for (int i = 0; i < itemAmount; i += 1)
            {
                if (user.Items == null)
                {
                    user.Items = Item.Id.ToString();
                }
                else
                {
                    user.Items += ";" + Item.Id;
                }
            }

            //testing, remove in production
            System.Threading.Thread.Sleep(500);

            IdentityResult identityResult = await userManager.UpdateAsync(user);

            if (identityResult.Succeeded)
            {
                return new JsonResult(Item.ItemName + ";" + Item.Price + ";" + amount + ";" + Item.EncryptedId);
            }
            else return null;            
        }
    }
}