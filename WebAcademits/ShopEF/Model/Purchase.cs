using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Console;

namespace ShopEF.Model
{
    public class Purchase : IEntity
    {
        public int Id { get; set; }

        [ConsoleAlias("Количество товара", true, true)]
        public int Count { get; set; }


        [ConsoleAlias("Название товара", true, true)]
        public virtual Product Product { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
