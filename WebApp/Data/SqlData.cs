using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApp.Models;

namespace WebApp
{
    public class SqlData : IDataAcces//, Data.IUserAcces
    {
        private ShopDbContext shopDbcontext;
        private readonly IDataProtector itemDataProtector;
        private readonly IDataProtector orderDataProtector;
        private IHostingEnvironment hostingEnvironment;

        public SqlData(
            ShopDbContext shopDbcontext,
            IDataProtectionProvider protectionProvider,
            PurposeStringConstants purposeStringConstants,
            IHostingEnvironment hostingEnvironment
            )
        {
            itemDataProtector = protectionProvider.CreateProtector(purposeStringConstants.ItemIdString);
            orderDataProtector = protectionProvider.CreateProtector(purposeStringConstants.OrderItemString);
            this.hostingEnvironment = hostingEnvironment;
            this.shopDbcontext = shopDbcontext;
        }

        public void AddItem(Item item)
        {
            shopDbcontext.Add(item);
            
            shopDbcontext.SaveChanges();

            //this seems really stupid
            //next time use string as id
            Item itemFromDb = shopDbcontext.Items.FirstOrDefault(i => i.ItemName == item.ItemName);
            item.EncryptedId = itemDataProtector.Protect(itemFromDb.Id.ToString());

            UpdateItem(itemFromDb);            
        }

        public void DeleteItem(int id)
        {
            Item item = GetItem(id);
            if (Directory.Exists(Path.Combine(hostingEnvironment.WebRootPath, "images", item.ItemName)))
            {
                File.Delete(hostingEnvironment.WebRootPath + item.PicturePath);
                IEnumerable<string> files = Directory.EnumerateFiles(Path.Combine(hostingEnvironment.WebRootPath, "images", item.ItemName));
                if (files.Count() == 0)
                {
                    Directory.Delete(Path.Combine(hostingEnvironment.WebRootPath, "images", item.ItemName));
                }
                else
                {
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    Directory.Delete(Path.Combine(hostingEnvironment.WebRootPath, "images", item.ItemName));
                }
            }
            shopDbcontext.Items.Remove(item);
            shopDbcontext.SaveChanges();
        }

        public IQueryable<Item> GetAllItems()
        {
            return shopDbcontext.Items.OrderBy(i => i.ItemName);
        }

        public Item GetItem(int id)
        {
            return shopDbcontext.Items.FirstOrDefault(i => i.Id == id);
        }

        public Item GetItem(string cryptedId)
        {
            int decryptedId = int.Parse(itemDataProtector.Unprotect(cryptedId));

            return shopDbcontext.Items.FirstOrDefault(i => i.Id == decryptedId);
        }

        public void UpdateItem(Item item)
        {
            shopDbcontext.Attach(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            shopDbcontext.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            shopDbcontext.Add(order);            
            shopDbcontext.SaveChanges();
        }

        public Order GetOrder(int id)
        {
            return shopDbcontext.Orders.FirstOrDefault(o => o.Id == id);
        }
        
        public Order GetLastOrder(int userId)
        {
            return shopDbcontext.Orders.Where(o => o.CustomerId == userId).OrderBy(d => d.DateTimeOrdered).Last();
        }

        public IQueryable<Order> GetOrdersOfUser(int userId)
        {
            return shopDbcontext.Orders.Where(o => o.CustomerId == userId);
        }

        public IQueryable<Order> GetOrders(OrderState orderState)
        {
            return shopDbcontext.Orders.Where(o => o.State == orderState);
        }

        public IQueryable<Order> GetOrders()
        {
            return shopDbcontext.Orders;            
        }

        public void UpdateOrder(Order order)
        {
            shopDbcontext.Attach(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            shopDbcontext.SaveChanges();
        }

        public void AddAuthor(Author author)
        {
            shopDbcontext.Authors.Add(author);
            shopDbcontext.SaveChanges();
        }

        public Author GetAuthor(string id)
        {
            return shopDbcontext.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void UpdateAuthor(Author author)
        {
            shopDbcontext.Attach(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            shopDbcontext.SaveChanges();
        }

        public IQueryable<Author> GetAuthors()
        {
            return shopDbcontext.Authors.OrderBy(n => n.Name);
        }

        public IQueryable<Item> GetLangItems(Lang lang)
        {
            return shopDbcontext.Items.Where(i => i.Language == lang);
        }

        public IQueryable<Item> GetLangCategoryItems(Lang lang, Category category)
        {
            return shopDbcontext.Items.Where(i => ((i.Language == lang) && (i.Category == category)));
        }

        public IQueryable<Item> GetItemsOfAuthor(string authorId)
        {
            return shopDbcontext.Items.Where(i => i.AuthorId == authorId).OrderBy(i => i.ItemName);
        }
    }
}
