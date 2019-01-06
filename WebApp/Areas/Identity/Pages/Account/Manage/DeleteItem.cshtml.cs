using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class DeleteItemModel : PageModel
    {        
        private IDataAcces dataAcces;
        
        [BindProperty]
        public Item Item { get; set; }

        public DeleteItemModel(IDataAcces dataAcces)
        {
            this.dataAcces = dataAcces;            
        }

        public IActionResult OnGet(string id)
        {
            Item = dataAcces.GetItem(id);

            if (Item == null)
            {
                return NotFound();
            }
            else return Page();
        }

        public IActionResult OnPost(int id)
        {
            dataAcces.DeleteItem(id);
            return RedirectToPage("Administration");                    
        }
    }
}