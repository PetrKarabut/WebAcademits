using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Console;

namespace ShopEF.Model
{
    public class Order : IEntity
    {
        public int Id { get; set; }

        [ConsoleAlias("Заказчик", false, true)]
        public virtual Buyer Buyer { get; set; }

        [ConsoleAlias("Дата", true, true)]
        public string Date { get; set; }

        [ConsoleAlias("Позиции", false, true)]
        public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}
