using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class AddAuthorModel : PageModel
    {
        private IDataAcces dataAcces;
        [BindProperty]
        public Author Author { get; set; }

        public AddAuthorModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
            Author = new Author();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                dataAcces.AddAuthor(Author);
                return RedirectToPage("Administration");
            }
            else
            {
                return Page();
            }
        }
    }
}