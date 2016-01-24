using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cursorGridDrawTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Ausgabe.ASCII();
            Console.ReadKey();
        }


        }
        class Ausgabe
        {
            public static void ASCII()
            {
                for (int x = 32; x < 255; x+=8)
			{
                Console.WriteLine();
                for (int y = x; y < x + 8; y++) Console.Write("{0,5},{1}", y, (char)y);
                

			}
            }
        }

}




