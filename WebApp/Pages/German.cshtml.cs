using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class GermanModel : PageModel
    {
        readonly IDataAcces dataAcces;
        public IQueryable<Item> Items { get; set; }
        public GermanModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
            Items = dataAcces.GetLangItems(Models.Lang.ger);
        }
        public void OnGet()
        {

        }
    }
}