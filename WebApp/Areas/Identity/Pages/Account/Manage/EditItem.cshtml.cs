using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    [Authorize(Roles = "Admin")]
    public class EditItemModel : PageModel
    {
        private IDataAcces dataAcces;
        private IHostingEnvironment hostingEnvironment;
        [BindProperty]
        public Item Item { get; set; }
        public IQueryable<Author> Authors { get; set; }
        public string OldPicturePath { get; set; }

        public EditItemModel(IDataAcces dataAcces, IHostingEnvironment hostingEnvironment)
        {
            this.dataAcces = dataAcces;
            this.hostingEnvironment = hostingEnvironment;
            Authors = dataAcces.GetAuthors();
        }

        public IActionResult OnGet(int id)
        {
            Item = dataAcces.GetItem(id);
            if (Item == null)
            {
                return NotFound();
            }
            else return Page();
        }

        public IActionResult OnPost(IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    Directory.CreateDirectory(Path.Combine(hostingEnvironment.WebRootPath, "images", Item.ItemName));
                    //HTML works with different kind of paths
                    Item.PicturePath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", Item.ItemName, file.FileName);
                    using (var stream = new FileStream(Path.Combine(hostingEnvironment.WebRootPath, "images", Item.ItemName, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                
                dataAcces.UpdateItem(Item);
                return RedirectToPage("/Details", new { id = Item.EncryptedId });
            }
            else return RedirectToPage("Error");
        }
    }
}