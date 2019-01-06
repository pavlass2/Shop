using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class CzechModel : PageModel
    {
        readonly IDataAcces dataAcces;
        public IQueryable<Item> Items { get; set; }
        public CzechModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
            Items = dataAcces.GetLangItems(Models.Lang.cze);
        }
        public void OnGet()
        {

        }
    }
}