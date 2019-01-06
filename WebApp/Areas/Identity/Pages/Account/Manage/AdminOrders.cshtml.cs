using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class AdminOrdersModel : PageModel
    {
        public IDataAcces DataAcces { get; set; }
    
        public IQueryable<Order> Orders { get; set; }
       

        public AdminOrdersModel(IDataAcces dataAcces)
        {
            DataAcces = dataAcces;
           
        }

        public void OnGet()
        {
          
            Orders = DataAcces.GetOrders();
        }
    }
}