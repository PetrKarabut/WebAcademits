using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Console;

namespace ShopEF.Model
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        [ConsoleAlias("Название", true, true)]
        public string Name { get; set; }

        [ConsoleAlias("Цена", false, true)]
        public int Price { get; set; }

        [ConsoleAlias("Категории", false, true)]
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
