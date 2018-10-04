using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = System.Configuration.ConfigurationManager.AppSettings["uri"];
            Console.WriteLine(uri);
            Console.Read();
        }
    }
}
