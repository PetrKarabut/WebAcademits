using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Console;

namespace ShopEF.Model
{
    public class Buyer : IEntity
    {
        public int Id { get; set; }

        [ConsoleAlias("Ф.И.О.", true, true)]
        public string Name { get; set; }

        [ConsoleAlias("Телефон", false, true)]
        public string Phone { get; set; }

        [ConsoleAlias("Электронная почта", false, true)]
        public string Email { get; set; }

        [ConsoleAlias("Заказы", false, false)]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
