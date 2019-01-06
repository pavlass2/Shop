using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class ShoppingCart
    {
        private List<string> itemsString;
        private IDataAcces dataAcces;
        public decimal TotalPrice { get; private set; }
        public int TotalItemsCount { get; private set; }
        public List<ItemInShoppingCart> ItemsInShoppingCart { get; private set; }

        public ShoppingCart(List<string> itemsString, IDataAcces dataAcces)
        {
            this.itemsString = itemsString;
            this.dataAcces = dataAcces;
            ItemsInShoppingCart = GetShoppingCart();
        }

        private List<ItemInShoppingCart> GetShoppingCart()
        {
            //list to return
            List<ItemInShoppingCart> itemsInShoppingCart = new List<ItemInShoppingCart>();
            //iter through all the items
            //i is const beacuase itemsString.Count is lowering itself by atleast one each iteration
            for (byte i = 0; i < itemsString.Count;)
            {
                //get first Item in shopping cart
                Item item = dataAcces.GetItem(int.Parse(itemsString.First()));
                //add price of this item to TotalPrice
                TotalPrice += item.Price;
                //add item to item Totalcount
                TotalItemsCount += 1;
                //remove the first item from the shopping cart
                itemsString.RemoveAt(0);
                //create ItemInShoppingCart to hold the data for viewing
                ItemInShoppingCart itemInShoppingCart = new ItemInShoppingCart(item.ItemName, item.Price, item.EncryptedId, item.PicturePath, item.Id);

                //check if there is the same item again, if so then count them and remove them
                //first create List that will hold the possible same items
                //List<string> sameItems = null;
                //while there is same id in the shopping cart string, do the following iteration
                while (itemsString.FirstOrDefault(id => id == item.Id.ToString()) != null)
                {
                    //sameItems = new List<string>();
                    //find the same item and save him
                    //sameItems.Add(itemsString.FirstOrDefault(id => id == item.Id.ToString()));
                    //remove th same item
                    itemsString.Remove(itemsString.First(id => id == item.Id.ToString()));
                    //increase the count of the ItemInShoppingCart
                    itemInShoppingCart.OneMore();
                    //add price of this item to TotalPrice
                    TotalPrice += item.Price;
                    //add item to item TotalCount
                    TotalItemsCount += 1;
                }
                itemsInShoppingCart.Add(itemInShoppingCart);
            }
            return itemsInShoppingCart;
        }
    }
}
