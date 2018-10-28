using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Console;
using ShopEF.Model;

namespace ShopEF
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Database.SetInitializer<Context>(new DbInitializer());
            using (var context = new Context())
            {
                System.Console.WriteLine("ИНИЦИАЛИЗАЦИЯ БАЗЫ ...");
                context.Categories.ToArray();
                System.Console.WriteLine("БАЗА ИНИЦИАЛИЗИРОВАНА");


                var console = new ConsoleFunctions();

                System.Console.WriteLine();
                System.Console.WriteLine("*** КАТЕГОРИИ ТОВАРОВ ***");
                PrintCategories(context, console);

                System.Console.WriteLine();
                System.Console.WriteLine("*** ТОВАРЫ ***");
                PrintProducts(context, console);

                System.Console.WriteLine();
                System.Console.WriteLine("*** ЗАКАЗЧИКИ ***");
                PrintClients(context, console);

                System.Console.WriteLine();
                System.Console.WriteLine("*** ЗАКАЗЫ ***");
                PrintOrders(context, console);

                EditPhone(context, "Иванов Иван Иваныч", "89662345236");

                //DeleteCategory(context, "Процессоры");


                System.Console.WriteLine();
                System.Console.WriteLine("*** НАИБОЛЕЕ ЧАСТО ПОКУПАЕМЫЙ ТОВАР ***");
                PrintMostPopularProduct(context, console);

                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("*** ТРАТЫ ПО КЛИЕНТАМ ***");
                PrintClientsSpendings(context, console);

                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("*** КУПЛЕНО ПО КАТЕГОРИЯМ ***");
                PrintCategoryMerchantability(context, console);
            }

            System.Console.Read();
        }


        private static void PrintCategories(Context context, ConsoleFunctions console)
        {
            var categories = context.Categories.ToArray();
            console.Write(categories);
        }

        private static void PrintProducts(Context context, ConsoleFunctions console)
        {
            foreach (var product in context.Products)
            {
                console.Write(product);
                System.Console.WriteLine();
            }
        }

        private static void PrintClients(Context context, ConsoleFunctions console)
        {
            foreach (var client in context.Buyers)
            {
                console.Write(client);
                System.Console.WriteLine();
            }
        }

        private static void PrintOrders(Context context, ConsoleFunctions console)
        {
            foreach (var order in context.Orders)
            {
                console.Write(order);
                System.Console.WriteLine();
            }
        }


        private static void EditPhone(Context context, string clientName, string newPhone)
        {
            var client = context.Buyers.Where(x => x.Name == clientName).ToArray().FirstOrDefault();
            if (client == null)
            {
                return;
            }

            client.Phone = newPhone;
            context.SaveChanges();
        }

        private static void DeleteCategory(Context context, string name)
        {
            var category = context.Categories.Where(x => x.Name == name).ToArray().FirstOrDefault();
            if (category == null)
            {
                return;
            }

            context.Categories.Remove(category);
            context.SaveChanges();
        }

        private static void PrintMostPopularProduct(Context context, ConsoleFunctions console)
        {
            var product = context.Purchases.GroupBy(x => x.Product).OrderByDescending(x => x.Sum(p => p.Count)).First()
                .Select(x => x.Product).First();
            console.Write(product);
        }

        private static void PrintClientsSpendings(Context context, ConsoleFunctions console)
        {
            var spendings = context.Buyers.Select(x => new
            {
                Client = x,
                Spent = x.Orders.SelectMany(o => o.Purchases).Sum(purchase => purchase.Product.Price * purchase.Count)
            });

            foreach (var spending in spendings)
            {
                System.Console.WriteLine();
                console.WriteShort(spending.Client);
                System.Console.Write(" " + spending.Spent);
            }
        }

        private static void PrintCategoryMerchantability(Context context, ConsoleFunctions console)
        {
            Func<Product, int> bought = product =>
                context.Purchases.Where(purchase => purchase.Product == product).Sum(purchase => purchase.Count);

            var merchantabilities = context.Categories.Select(x => new
            {
                Category = x,
                Bought = x.Products.Sum(product =>
                    context.Purchases.Where(purchase => purchase.Product == product).Sum(purchase => purchase.Count)
                    )
            });

            foreach (var merchantability in merchantabilities)
            {
                System.Console.WriteLine();
                console.WriteShort(merchantability.Category);
                System.Console.Write(" " + merchantability.Bought);
            }
        }
    }

}

