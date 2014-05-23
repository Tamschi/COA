using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COA
{
    class Program
    {
        static void Main(string[] args)
        {
            Ass.Load();
            using (var reader = new StreamReader(Ass.R.test))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
            Console.ReadLine();
        }
    }
}
