using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEF.Model
{
    public class Purchase
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
