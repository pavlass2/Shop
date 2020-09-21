using System.ComponentModel.DataAnnotations;
using System.IO;
using WebApp.Models;

namespace WebApp
{
    public class Item
    {
        //Picture is stored in "wwwroot/images/[name of the item]"
        //retrospectivaly I think that storing the image its self in the database could be the better aproach 
        private string _picturePath;

        [Display(Name = "Author")]
        public string AuthorId { get; set; }

        /// <summary>
        /// Identity key
        /// </summary>
        [Required]
        public int Id { get; set; }

        public string EncryptedId { get; set; }

        /// <summary>
        /// Name of the item
        /// </summary>        
        [Required, StringLength(255)]
        [Display(Name="Item name")]
        public string ItemName { get; set; }

        /// <summary>
        /// Language in which is the item written in
        /// </summary>
        public Lang Language { get; set; }

        /// <summary>
        /// Category of the item
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Item description
        /// </summary>
        public string Descriotion { get; set; }
        
        /// <summary>
        /// Path to the picture of the item
        /// </summary>
        public string PicturePath
        {
            //If there is no image of the item stored in _picturePath, use the generic image for file
            get
            {
                string slash = Path.DirectorySeparatorChar.ToString();
                if (_picturePath == null)
                {
                    return new string(slash + "images" + slash + "NoImage.png");
                }
                else return _picturePath;
            }
            set
            {
                _picturePath = value;
            }
        }        
        
        /// <summary>
        /// Price of the item
        /// </summary>
        public decimal Price { get; set; }

        public override string ToString()
        {
            return ItemName;
        }
    }
}
