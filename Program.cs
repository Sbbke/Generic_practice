using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBF_calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Elements> elements = new List<Elements>();
            string ElementsPath = @"C:\Users\User\Desktop\C#\day3\GBF-calculator\temp\GBF.txt";
            var newElements = BasicDataGeneric.LoadFile<Elements>(ElementsPath);


            while (true)
            {
                double total = 0.0;
                double drop = 0.0;
                double rate = 0.0;

                foreach (var ele in newElements)
                {


                    total += ele.number;
                    drop = drop + ele.ssr1 + ele.ssr2 + ele.ssr3;
                    rate = drop / total;

                    Console.WriteLine($"{ele.element}:{ele.number},{ele.ssr1},{ele.ssr2},{ele.ssr3}");
                }

                Console.WriteLine($"total number is {total}     total drops is {drop}\ndrop rate = {rate}\n");

                Console.WriteLine("enter exit to save and quit edit or press any botton on the keyboard to keep editing\n");
                var enter = Console.ReadLine();
                if(enter == "exit")
                {
                    break;
                }

                elements = BasicDataGeneric.EditFile(newElements);

                Console.Clear();

            }

            BasicDataGeneric.SaveFile(elements, ElementsPath);
            Console.WriteLine("Save successfully");
            Console.ReadLine();
        }

    }

}
