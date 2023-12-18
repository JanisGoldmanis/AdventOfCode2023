using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day8
    {
        static double gcf(double a, double b)
        {
            while (b != 0)
            {
                double temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static double lcm(double a, double b)
        {
            return (a / gcf(a, b)) * b;
        }


        public void Part1()
        {
            string filePath = "Input/Day8.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day8 Part1");

            char[] instructions = dataList[0].ToCharArray();
            string[] fullLocationData = dataList.Skip(2).ToArray();

            string[] locations = new string[fullLocationData.Length];
            Tuple<string, string>[] leftRight = new Tuple<string, string>[fullLocationData.Length];

            Dictionary<string, int> keyIndex = new Dictionary<string, int>();

            for (int i = 0; i<fullLocationData.Length; i++)
            {
                string location = fullLocationData[i];
                string[] parts = location.Split('=');
                locations[i] = parts[0].Trim();
                string[] tupleParts = parts[1].Trim().Trim('(', ')').Split(',');
                leftRight[i] = Tuple.Create(tupleParts[0].Trim(), tupleParts[1].Trim());
                keyIndex[locations[i]] = i;
            }

            int counter = 0;
            int instructionLength = instructions.Length;

            string currentLocation = "AAA";

            while (true)
            {
                char instruction = instructions[counter % instructionLength];
                int index = keyIndex[currentLocation];

                if (instruction == 'R')
                {
                    currentLocation = leftRight[index].Item2;
                }
                if (instruction == 'L')
                {
                    currentLocation = leftRight[index].Item1;
                }
                counter++;
                if (currentLocation == "ZZZ")
                {
                    break;
                }
            }
            Console.WriteLine(counter);
        }


        public void Part2()
        {
            string filePath = "Input/Day8.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day8 Part2");

            char[] instructions = dataList[0].ToCharArray();
            string[] fullLocationData = dataList.Skip(2).ToArray();

            string[] locations = new string[fullLocationData.Length];
            Tuple<string, string>[] leftRight = new Tuple<string, string>[fullLocationData.Length];

            Dictionary<string, int> keyIndex = new Dictionary<string, int>();

            for (int i = 0; i < fullLocationData.Length; i++)
            {
                string location = fullLocationData[i];
                string[] parts = location.Split('=');
                locations[i] = parts[0].Trim();
                string[] tupleParts = parts[1].Trim().Trim('(', ')').Split(',');
                leftRight[i] = Tuple.Create(tupleParts[0].Trim(), tupleParts[1].Trim());
                keyIndex[locations[i]] = i;
            }


            int instructionLength = instructions.Length;

            List<string> currentLocations = new List<string>();

            foreach (string location in locations)
            {
                if (location.EndsWith('A'))
                {
                    currentLocations.Add(location);
                }
            }

            string[] currentLocationsArr = currentLocations.ToArray();

            List<int> cycleLengths = new List<int>();            

            foreach( string location in currentLocationsArr)
            {
                int counter = 0;
                string currentLocation = location;

                while (true)
                {
                    char instruction = instructions[counter % instructionLength];
                    int index = keyIndex[currentLocation];

                    if (instruction == 'R')
                    {
                        currentLocation = leftRight[index].Item2;
                    }
                    if (instruction == 'L')
                    {
                        currentLocation = leftRight[index].Item1;
                    }
                    counter++;
                    if (currentLocation.EndsWith('Z'))
                    {
                        cycleLengths.Add(counter);
                        break;
                    }
                }                            
            }
            Console.WriteLine(cycleLengths.Count);

            double current = (double)cycleLengths[0];

            for (int i = 1; i< cycleLengths.Count; i++)
            {
                current = lcm(current, (double) cycleLengths[i]);
            }
            Console.WriteLine(current);
        }            
    }
}
