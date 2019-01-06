using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class AdminItemsModel : PageModel
    {
        private readonly IDataAcces dataAcces;
        public IQueryable<Item> Items { get; set; }
        public AdminItemsModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
            Items = dataAcces.GetAllItems().OrderBy(i => i.ItemName);
        }

        public void OnGet()
        {
        }
    }
}