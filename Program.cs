using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace ConsoleChart
{
    class Program
    {
        static Dictionary<string, int> grouped = new Dictionary<string, int>();
        static Dictionary<string, int> result = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            string toGroup = args[0];
            bool maxRowsGiven = args.Length > 2;
            int maxLinesToShow = -1;
            if (maxRowsGiven) { maxLinesToShow = int.Parse(args[2]); }
            int tempOriHeight = Console.WindowHeight;

            realLines(args);

            var trimmedList = grouped.ToList();
            trimmedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            if (maxRowsGiven)
            {
                if (maxLinesToShow < trimmedList.Count)
                {
                    trimmedList.RemoveRange(maxLinesToShow, trimmedList.Count - (maxLinesToShow));
                }
            }

            //percent calculating
            int max = trimmedList[0].Value;
            foreach (var item in trimmedList)
            {
                result.Add(item.Key, (100 * item.Value) / max);
            }

            output();
        }


        public static void realLines(string[] args)
        {
            string[] splitedLine;
            int indexGroup = -1;
            int indexSum = -1;

            splitedLine = Console.ReadLine().Split('\t');
            string[] headLine = new string[splitedLine.Length];
            for (int i = 0; i < splitedLine.Length; i++)
            {
                headLine[i] = splitedLine[i];
                if (args[0] == headLine[i]) { indexGroup = i; }
                else if (args[1] == headLine[i]) { indexSum = i; }
            }
            for (int i = 1; true; i++)
            {
                try { splitedLine = Console.ReadLine().Split('\t'); }
                catch { break; }

                if (!grouped.ContainsKey(splitedLine[indexGroup]))
                {
                    grouped.Add(splitedLine[indexGroup], int.Parse(splitedLine[indexSum]));
                }
                else
                {
                    grouped[splitedLine[indexGroup]] += int.Parse(splitedLine[indexSum]);
                }
            }
        }

        public static void output()
        {
            foreach (var item in result)
            {
                Console.Write($"{item.Key,41} | ");
                Console.BackgroundColor = ConsoleColor.Red;
                for (int i = 0; i < item.Value; i++)
                {
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
    }
}