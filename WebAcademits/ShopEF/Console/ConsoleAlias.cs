using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEF.Console
{
    public class ConsoleAlias : System.Attribute
    {
        public string Alias { get; set; }

        public bool UseInShortForm { get; set; }

        public bool UseInReading { get; set; }

        public ConsoleAlias(string alias, bool useInShortForm, bool useInReading)
        {
            Alias = alias;
            UseInReading = useInReading;
            UseInShortForm = useInShortForm;
        }
    }
}
