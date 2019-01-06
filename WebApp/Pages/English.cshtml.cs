using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class EnglishModel : PageModel
    {
        readonly IDataAcces dataAcces;
        public IQueryable<Item> Items { get; set; }
        public EnglishModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
            Items = dataAcces.GetLangItems(Models.Lang.eng);
        }
        public void OnGet()
        {

        }
    }
}