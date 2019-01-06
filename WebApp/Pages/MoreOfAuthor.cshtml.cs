using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class MoreOfAuthorModel : PageModel
    {
        IDataAcces dataAcces;
        public Author Author { get; set; }
        public IQueryable<Item> Items { get; set; }
        public MoreOfAuthorModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
        }
        public IActionResult OnGet(string id)
        {
            Author = dataAcces.GetAuthor(id);
            Items = dataAcces.GetItemsOfAuthor(Author.Id);
            return Page();
        }
    }
}