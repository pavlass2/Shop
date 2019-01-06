using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class AuthorsModel : PageModel
    {
        private IDataAcces dataAcces;
        public IQueryable<Author> Authors { get; set; }
        public AuthorsModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
        }
        public void OnGet()
        {
            Authors = dataAcces.GetAuthors();
        }
    }
}