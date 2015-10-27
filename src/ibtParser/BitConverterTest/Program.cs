using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitConverterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //
            // Create an array of four bytes.
            // ... Then convert it into an integer and unsigned integer.
            //
            byte[] array = new byte[4];
            array[0] = 1; // Lowest
            array[1] = 64;
            array[2] = 0;
            array[3] = 0; // Sign bit
                          //
                          // Use BitConverter to convert the bytes to an int and a uint.
                          // ... The int and uint can have different values if the sign bit differs.
                          //
            int result1 = BitConverter.ToInt32(array, 0); // Start at first index
            uint result2 = BitConverter.ToUInt32(array, 0); // First index
            Console.WriteLine(result1);
            Console.WriteLine(result2);
            Console.WriteLine("int");
            int v = 300;
            var b = BitConverter.GetBytes(v);
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(String.Format("{0} {1}", b[i].ToString(), b[i].ToString("X")));
            }
            var v2 = BitConverter.ToInt32(b, 0);
            Console.WriteLine(v2.ToString());

            Console.WriteLine("float");
            float f = 300;
            b = BitConverter.GetBytes(f);
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(String.Format("{0} {1}",b[i].ToString(), b[i].ToString("X")));
            }
            var f2 = BitConverter.ToSingle(b,0);
            Console.WriteLine(f2.ToString());

            Console.WriteLine("double");
            double d = 300;
            b = BitConverter.GetBytes(d);
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(String.Format("{0} {1}", b[i].ToString(), b[i].ToString("X")));
            }
            var d2 = BitConverter.ToSingle(b, 0);
            Console.WriteLine(d2.ToString());

            Console.ReadLine();

        }
    }
}
