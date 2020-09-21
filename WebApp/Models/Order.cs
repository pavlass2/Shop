using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public enum OrderState { Ordered, Dispatched, Delivered };

    public enum Delivery { Fast, Cheap, Free };

    public enum Payment { Card, Paypal };

    public class Order
    {
        public int Id { get; set; }

        //not used
        public string EncryptedId { get; set; }

        public int CustomerId { get; private set; }      
        
        //stored as: item;item;item
        public string Items { get; private set; }

        public DateTime DateTimeOrdered { get; private set; }

        public DateTime DateTimeDispatched { get; private set; }

        public DateTime DateTimeDelivered { get; private set; }

        public OrderState State { get; private set; }

        public Delivery Delivery { get; private set; }

        public Payment Payment { get; private set; }

        public decimal TotalPrice { get; private set; }

        public string CustomerName { get; set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public string FirstName { get; private set; }

        public string SecondName { get; private set; }

        public string AddressStreet { get; private set; }

        public string AddressHouseNumber { get; private set; }

        public string AddressCity { get; private set; }

        public string AddressCountry { get; private set; }

        public string AddressZip { get; private set; }

        public Order()
        {

        }

        public Order(
            int customerId,
            ShoppingCart shoppingCart,
            Delivery delivery,
            Payment payment,
            string customerName,
            string email,
            string phoneNumber,
            string firstName,
            string secondName,
            string addressStreet,
            string addressHouseNumber,
            string addressCity,
            string addressCountry,
            string addressZip
            )
        {
            CustomerId = customerId;
            DateTimeOrdered = DateTime.Now;
            State = OrderState.Ordered;
            Delivery = delivery;
            Payment = payment;
            CustomerName = customerName;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            SecondName = secondName;
            AddressStreet = addressStreet;
            AddressHouseNumber = addressHouseNumber;
            AddressCity = addressCity;
            AddressCountry = addressCountry;
            AddressZip = addressZip;

            //set Items

            List<string> ItemsList = new List<string>();
            for (int i = 0; i < shoppingCart.ItemsInShoppingCart.Count; i++)
            {
                if (i == 0)
                {
                    Items = shoppingCart.ItemsInShoppingCart[0].Id.ToString();
                    for (int j = 1; j < shoppingCart.ItemsInShoppingCart[i].Count; j++) //if the first item is more inserted more than once
                    {
                        Items += ";" + shoppingCart.ItemsInShoppingCart[i].Id;
                    }
                }
                else
                {
                    for (int j = 0; j < shoppingCart.ItemsInShoppingCart[i].Count; j++) //if the item is inserted more than once
                    {
                        Items += ";" + shoppingCart.ItemsInShoppingCart[i].Id;
                    }
                }
            }
            TotalPrice = shoppingCart.TotalPrice;
        }

        public void ChangeStateToDispatched()
        {
            State = OrderState.Dispatched;
            DateTimeDispatched = DateTime.Now;
        }

        public void ChangeStateToDelivered()
        {
            State = OrderState.Delivered;
            DateTimeDelivered = DateTime.Now;
        }
    }
}
