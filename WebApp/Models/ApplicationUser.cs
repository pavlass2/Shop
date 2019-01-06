using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        private string _addressCountry;
        private string _addressCity;
        private string _addressStreet;
        private string _addressHouseNumber;
        private string _addressZip;
        private string _items;
 

        #region Getters and Setters
        public string AddressCountry
        {
            get
            {
                if (_addressCountry == null)
                {
                    return new string("");
                }
                else
                {
                    return _addressCountry;
                }
            }
            set => _addressCountry = value;
        }        

        public string AddressCity
        {
            get
            {
                if (_addressCity == null)
                {
                    return new string("");
                }
                else
                {
                    return _addressCity;
                }
            }
            set => _addressCity = value;
        }

        public string AddressStreet
        {
            get
            {
                if (_addressStreet == null)
                {
                    return new string("");
                }
                else
                {
                    return _addressStreet;
                }
            }
            set => _addressStreet = value;
        }

        public string AddressHouseNumber
        {
            get
            {
                if (_addressHouseNumber == null)
                {
                    return new string("");
                }
                else
                {
                    return _addressHouseNumber;
                }
            }
            set => _addressHouseNumber = value;
        }

        public string AddressZip
        {
            get
            {
                if (_addressZip == null)
                {
                    return new string("");
                }
                else
                {
                    return _addressZip;
                }
            }
            set => _addressZip = value;
        }

        public string Items
        {
            get
            {
                if (_items == "")
                {
                    return null;
                }
                else
                {
                    return _items;
                }
            }
            set
            {
                _items = value;
            }
        }

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        #endregion        

        #region ShoppingCart
        /// <summary>
        /// Get all items in user's cart
        /// </summary>
        /// <returns>List of items in users's cart</returns>
        public List<string> GetItems()
        {
            if (Items == null)
            {
                return null;
            }
            else
            {
                return Items.Split(";").ToList();
            }
        }

        /// <summary>
        /// Returns number of occurance
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int IsInShoppingCart(int id)
        {
            if (Items != null)
            {
                List<string> itemsString = Items.Split(";").ToList();
                return itemsString.FindAll(i => i == id.ToString()).Count;
            }
            else return 0;
        }
        
        public void EraseAll()
        {
            Items = "";
        }

        public string EraseItem(Item item)
        {
            if (Items != null)
            {
                List<string> itemsList = Items.Split(";").ToList();
                if (itemsList.Contains(item.Id.ToString()))
                {
                    itemsList.Remove(item.Id.ToString());
                    EraseAll();
                    foreach (string s in itemsList)
                    {
                        if (Items == null)
                        {
                            Items = s;
                        }
                        else
                        {
                            Items += ";" + s;
                        }
                    }
                    return Items;
                }
                return null;
            }
            return null;
        }

        public void EraseItems(List<Item> itemsToRemoveList)
        {
            List<string> itemsList = Items.Split(";").ToList();
            foreach (Item item in itemsToRemoveList)
            {
                if (itemsList.Contains(item.Id.ToString()))
                {
                    itemsList.Remove(item.Id.ToString());
                }
            }
            _items = itemsList[0];
            foreach (string s in itemsList)
            {
                Items = Items + ";" + s;
            }
        }
        #endregion
    }

}
