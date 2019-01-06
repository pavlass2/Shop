using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class EditAuthorModel : PageModel
    {
        private readonly IDataAcces dataAcces;
        [BindProperty]
        public Author Author { get; set; }
        public EditAuthorModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;
        }
        public IActionResult OnGet(string id)
        {
            Author = dataAcces.GetAuthor(id);
            if (Author == null)
            {
                return NotFound();
            }
            else return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                dataAcces.UpdateAuthor(Author);
                return RedirectToPage("Administration");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}