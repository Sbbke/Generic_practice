using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBF_calculator
{
    class BasicDataGeneric
    {
        public static List<T> LoadFile<T>(string filePath) where T : class, new()
        {
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            List<T> output = new List<T>();
            T entry = new T();
            var cols = entry.GetType().GetProperties();
            if (lines.Count < 6)
            {
                throw new IndexOutOfRangeException("The file is missing something");
            }
            var headers = lines[0].Split(',');
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();
                var values = row.Split(',');
                for (var i = 0; i < headers.Length; i++)
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == headers[i])
                        {
                            col.SetValue(entry, Convert.ChangeType(values[i], col.PropertyType));
                        }
                    }
                }
                output.Add(entry);
            }
            return output;

        }
        public static List<Elements> EditFile(List<Elements> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("data", "the data cannot be empty");

            }

            Console.WriteLine("Chose an element:\n1:火  2:水  3:土  4:風  5:光  6:暗");
            int index = Convert.ToInt32(Console.ReadLine());
            index--;
            Console.WriteLine("Number increase:");
            var num = Convert.ToInt32(Console.ReadLine());
            data[index].number += num;
            Console.WriteLine("is there a weapon drop?(yes/no)");
            var input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("Which weapon?");
                int weaponNum = Convert.ToInt32(Console.ReadLine());
                switch (weaponNum)
                {
                    case 1: data[index].ssr1 += 1; break;
                    case 2: data[index].ssr2 += 1; break;
                    case 3: data[index].ssr3 += 1; break;
                }

            }

            return data;
        }

        public static void SaveFile<T>(List<T> data, string filepath) where T : class
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();
            if (data == null || data.Count == 0)
            {
                throw new ArgumentNullException("data", "the data cannot be empty");

            }
            var cols = data[0].GetType().GetProperties();

            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }
            lines.Add(line.ToString().Substring(0, line.Length - 1));//remove last comma
            foreach (var row in data)
            {
                line = new StringBuilder();
                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }
                lines.Add(line.ToString().Substring(0, line.Length - 1));//remove last comma
            }
            System.IO.File.WriteAllLines(filepath, lines);
        }
    }
}
