using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class DeliveryModel : PageModel
    {
        public UserManager<ApplicationUser> UserManager { get; set; } 
        public ApplicationUser ApplicationUser { get; set; }
        public IDataAcces DataAcces { get; set; }
        public List<Item> Items { get; set; }
        public string Delivery { get; set; }
        public string Payment { get; set; }

        public DeliveryModel (UserManager<ApplicationUser> userManager, IDataAcces dataAcces)
        {
            UserManager = userManager;
            DataAcces = dataAcces;
            Items = new List<Item>();
        }
        public IActionResult OnGet()
        {/*
            ApplicationUser = await UserManager.GetUserAsync(User);

            foreach (string s in ApplicationUser.GetItems())
            {
                Items.Add(DataAcces.GetItem(int.Parse(s)));
            }*/

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Delivery delivery;
            Payment payment;
            if (Delivery == "delivery1")
            {
                delivery = Models.Delivery.Fast;
            }
            else if(Delivery == "delivery2")
            {
                delivery = Models.Delivery.Cheap;
            }
            else
            {
                delivery = Models.Delivery.Free;
            }

            if (Payment == "payment1")
            {
                payment = Models.Payment.Card;
            }
            else
            {
                payment = Models.Payment.Paypal;
            }

            ApplicationUser = await UserManager.GetUserAsync(User);
            ShoppingCart shoppingCart = new ShoppingCart(ApplicationUser.GetItems(), DataAcces);
            Order order = new Order(
                ApplicationUser.Id,
                shoppingCart,
                delivery,
                payment,
                ApplicationUser.UserName,
                ApplicationUser.Email,
                ApplicationUser.PhoneNumber,
                ApplicationUser.FirstName,
                ApplicationUser.SecondName,
                ApplicationUser.AddressStreet,
                ApplicationUser.AddressHouseNumber,
                ApplicationUser.AddressCity,
                ApplicationUser.AddressCountry,
                ApplicationUser.AddressZip);
            ApplicationUser.Items = "";
            await UserManager.UpdateAsync(ApplicationUser);
            DataAcces.AddOrder(order);
            return RedirectToPage("OrderSummary");
        }
    }
}