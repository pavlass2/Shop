using System.Linq;
using WebApp.Models;

namespace WebApp
{
    public interface IDataAcces
    {
        /// <summary>
        /// Gets the spesific item
        /// </summary>
        /// <param name="Id">Id of the item</param>
        /// <returns>Item</returns>
        Item GetItem(int Id);

        /// <summary>
        /// Gets the specific item by crypted Id
        /// </summary>
        /// <param name="cryptedId">Crypted Id of the item</param>
        /// <returns>Item</returns>
        Item GetItem(string cryptedId);

        /// <summary>
        /// Gets items of specified author
        /// </summary>
        /// <param name="authorId">Id of author to get items from</param>
        /// <returns>IQuryable of items of the specified author</returns>
        IQueryable<Item> GetItemsOfAuthor(string authorId);

        /// <summary>
        /// Gets items in specified language
        /// </summary>
        /// <param name="lang">Language of the items</param>
        /// <returns>IQuryable of items in the specified language</returns>
        IQueryable<Item> GetLangItems(Lang lang);

        /// <summary>
        /// Gets items of specified category in specified language 
        /// </summary>
        /// <param name="lang">Language of the items</param>
        /// <param name="category">Category of the items</param>
        /// <returns>IQueryable of specified items</returns>
        IQueryable<Item> GetLangCategoryItems(Lang lang, Category category);

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>List of all items</returns>
        IQueryable<Item> GetAllItems();

        /// <summary>
        /// Adds an item
        /// </summary>
        /// <param name="item">Item to add</param>
        void AddItem(Item item);

        /// <summary>
        /// Updates the specific item
        /// </summary>
        /// <param name="item">Item to change</param>
        void UpdateItem(Item item);

        /// <summary>
        /// Delets the specific item
        /// </summary>
        /// <param name="Id">Id of the item</param>
        void DeleteItem(int Id);

        /// <summary>
        /// Adds an order
        /// </summary>
        /// <param name="order">Order to add</param>
        void AddOrder(Order order);

        /// <summary>
        /// Gets specified order
        /// </summary>
        /// <param name="id">Id of the order</param>
        /// <returns>The specified order</returns>
        Order GetOrder(int id);

        /// <summary>
        /// Gets last added order of the user
        /// </summary>
        /// <param name="userId">Id of the user to get the last order from</param>
        /// <returns>The specified order</returns>
        Order GetLastOrder(int userId);

        /// <summary>
        /// Gets all orders of the specified user
        /// </summary>
        /// <param name="userId">User to get the orders from</param>
        /// <returns>IQueryable of the orders from the specified user</returns>
        IQueryable<Order> GetOrdersOfUser(int userId);

        /// <summary>
        /// Gets all orders
        /// </summary>
        /// <returns>IQueryable of all orders</returns>
        IQueryable<Order> GetOrders();

        /// <summary>
        /// Gets all orders in specified state
        /// </summary>
        /// <param name="orderState">State of the orders</param>
        /// <returns>IQueryable of the orders in the specified state</returns>
        IQueryable<Order> GetOrders(OrderState orderState);

        /// <summary>
        /// Updates order
        /// </summary>
        /// <param name="order">Order to update</param>
        void UpdateOrder(Order order);

        /// <summary>
        /// Adds and author
        /// </summary>
        /// <param name="author">Author to add</param>
        void AddAuthor(Author author);

        /// <summary>
        /// Gets specified author
        /// </summary>
        /// <param name="id">Id of the author to get</param>
        Author GetAuthor(string id);

        /// <summary>
        /// Gets all authors
        /// </summary>
        /// <returns>IQueryable of all authors</returns>
        IQueryable<Author> GetAuthors();

        /// <summary>
        /// Updates specified author
        /// </summary>
        /// <param name="author">Author to update</param>
        void UpdateAuthor(Author author);  
    }
}
