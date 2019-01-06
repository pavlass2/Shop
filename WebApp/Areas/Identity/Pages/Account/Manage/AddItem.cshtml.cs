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
    public class AddItemModel : PageModel
    {
        [BindProperty]
        public Item Item { get; set; }
        //public Author Author { get; set; }

        private IFormFile formFile;
        private IDataAcces dataAcces;
        private IHostingEnvironment hostingEnvironment;
        public IQueryable<Author> Authors { get; set; }


        public AddItemModel(IDataAcces dataAcces, IHostingEnvironment hostingEnvironment)
        {
            Item = new Item();
            this.hostingEnvironment = hostingEnvironment;
            this.dataAcces = dataAcces;
            Authors = dataAcces.GetAuthors();
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost(IFormFile file = null)
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
                

                dataAcces.AddItem(Item);                
                return RedirectToPage("Administration");
            }
            else
            {
                return Page();
            }
        }
    }
}