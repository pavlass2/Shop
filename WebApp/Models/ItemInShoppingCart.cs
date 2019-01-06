namespace WebApp.Models
{
    public class ItemInShoppingCart
    {
        public int Count { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        private readonly decimal originalPrice;
        public string CryptedId { get; private set; }
        public string PicturePath { get; private set; }
        public int Id { get; set; }

        public ItemInShoppingCart(string name, decimal price, string cryptedId, string picturePath, int id)
        {
            Count = 1;
            Name = name;
            Price = price;
            originalPrice = price;
            CryptedId = cryptedId;
            PicturePath = picturePath;
            Id = id;
        }

        public void OneMore()
        {
            Count += 1;
            Price += originalPrice;
        }
    }
}
