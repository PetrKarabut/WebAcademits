using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Console;

namespace ShopEF.Model
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        [ConsoleAlias("Название", true, true)]
        public string Name { get; set; }

        [ConsoleAlias("Список товаров данной категории", false, false)]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();


    }
}
