using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        public List<Item> Items { get; set; }

        public IndexModel(IDataAcces data)
        {
            Items = data.GetAllItems().ToList();
        }

        public void OnGet()
        {
        }
    }
}
