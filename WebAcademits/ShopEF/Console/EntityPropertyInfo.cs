using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEF.Console
{
    public struct EntityPropertyInfo
    {
        public object Value { get; set; }

        public string Alias { get; set; }

        public bool UseInShortForm { get; set; }

        public bool UseInReading { get; set; }
    }
}
