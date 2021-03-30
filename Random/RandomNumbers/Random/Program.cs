using System;
using System.Collections.Generic;
using System.IO;

namespace RandomMain
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Int32> Numbers = new List<Int32>();
            Random random = new Random();

            for (int i = 0; i < 100001; i++)
            {
                var number = random.Next(Int32.MinValue, Int32.MaxValue);
                if (!Numbers.Contains((number)))
                {
                    Numbers.Add(number);
                }

            }             

            PrintFile(Numbers);
        }

         

        static void PrintFile(IEnumerable<int> numbers)
        {
            string path = @"C:\Users\je\Desktop\pruebaInercya\pruebaincercya\Random\numbers.txt"; 

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    foreach (var number in numbers)
                    {
                        tw.WriteLine(number);
                    }
                    tw.Close();
                }

            }
            else if (File.Exists(path))
            {

                using (var tw = new StreamWriter(path, false))
                {
                    foreach (var number in numbers)
                    {
                        tw.WriteLine(number);
                    }
                    tw.Close();
                }
            }
        }
    }
}
 