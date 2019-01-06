using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class AdminOrderModel : PageModel
    {
        public IDataAcces DataAcces { get; set; }

        public Order Order { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public AdminOrderModel(IDataAcces dataAcces)
        {
            DataAcces = dataAcces;
        }

        public IActionResult OnGet(int id)
        {
            Order = DataAcces.GetOrder(id);
            if (Order == null)
            {
                return NotFound();
            }
            else
            {
                ShoppingCart = new ShoppingCart(Order.Items.Split(";").ToList(), DataAcces);
                return Page();
            }
        }

        public void OnPost(int id)
        {
            Order = DataAcces.GetOrder(id);
            ShoppingCart = new ShoppingCart(Order.Items.Split(";").ToList(), DataAcces);
            
            if(Order.State == OrderState.Ordered)
            {
                Order.ChangeStateToDispatched();
            }
            else
            {
                Order.ChangeStateToDelivered();
            }
            DataAcces.UpdateOrder(Order);
        }
    }
}