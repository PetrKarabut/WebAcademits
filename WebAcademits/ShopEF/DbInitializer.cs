using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Model;

namespace ShopEF
{
    public class DbInitializer : CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            var categoryNames = new[] { "Комплектующие", "Процессоры", "Блоки питания" };
            var categories = categoryNames.Select(x => new Category { Name = x }).ToList();
            categories.ForEach(x => context.Categories.Add(x));

            var random = new Random();
            var processorsNames = new[] { "AMD 3850", "Intel Celeron-G3900" };
            var processors = processorsNames
                .Select(x => new Product { Name = x, Categories = categories.Take(2).ToList(), Price = random.Next(5000, 10000) }).ToList();
            processors.ForEach(x => context.Products.Add(x));

            var blockNames = new[] { "Aerocool KCAS-1000M", "Chieftec GPM-1000C" };
            var blocks = blockNames.Select(x => new Product
            { Name = x, Categories = new List<Category> { categories[0], categories[2] }, Price = random.Next(5000, 10000) }).ToList();
            blocks.ForEach(x => context.Products.Add(x));

            var client1 = new Buyer { Name = "Иванов Иван Иваныч", Email = "ivanov@gmail.com", Phone = "67235467821547" };
            var client2 = new Buyer { Name = "Евлампий Конопатов", Email = "evlamp@mail.ru", Phone = "45365456465" };
            var client3 = new Buyer { Name = "Бывшая Юлия Макаровна", Email = "yulia@yandex.ru", Phone = "9085092309" };

            new List<Buyer> { client1, client2, client3 }.ForEach(x => context.Buyers.Add(x));

            var order1 = new Order
            {
                Buyer = client1,
                Date = "12.08.2018",
                Purchases = new List<Purchase>
                    {
                        new Purchase {Product = processors[0], Count = 2},
                        new Purchase {Product = blocks[0], Count = 1}
                    }
            };

            var order2 = new Order
            {
                Buyer = client1,
                Date = "10.09.2018",
                Purchases = new List<Purchase>
                    {
                        new Purchase {Product = processors[1], Count = 2},
                        new Purchase {Product = blocks[0], Count = 3}
                    }
            };

            var order3 = new Order
            {
                Buyer = client2,
                Date = "5.03.2018",
                Purchases = new List<Purchase>
                    {
                        new Purchase {Product = processors[1], Count = 5},
                    }
            };

            var order4 = new Order
            {
                Buyer = client3,
                Date = "15.07.2018",
                Purchases = new List<Purchase>
                    {
                        new Purchase {Product = processors[0], Count = 1},
                        new Purchase{Product = processors[1], Count = 1}
                    }
            };

            new List<Order> { order1, order2, order3, order4 }.ForEach(x => context.Orders.Add(x));

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
