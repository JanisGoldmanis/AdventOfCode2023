using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day6
    {     
        public void Part1()
        {
            string filePath = "Input/Day6Example.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day6 Part1");


            string[] timeParts = dataList[0].Split(":");
            string[] timeStrings = timeParts[1].Trim().Split(" ");
            List<int> times = new List<int>();

            foreach (string timeString in timeStrings)
            {
                if (timeString.Length > 0)
                {
                    times.Add(int.Parse(timeString));
                }
            }

            string[] distanceParts = dataList[1].Split(":");
            string[] distanceStrings = distanceParts[1].Trim().Split(" ");
            List<int> distances = new List<int>();

            foreach (string distanceString in distanceStrings)
            {
                if (distanceString.Length > 0)
                {
                    distances.Add(int.Parse(distanceString));
                }
            }

            List<int> results = new List<int>();

            for (int i = 0; i < times.Count; i++)
            {
                int time = times[i];
                int distance = distances[i];

                int max = (int)Math.Ceiling(((-time - Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / -2) - 1);
                int min = (int)Math.Floor(((-time + Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / -2) + 1);

                int timeDifference = max - min + 1;

                results.Add(timeDifference);
            }

            int result = 1;

            foreach (int number in results)
            {
                result *= number;
            }

            Console.WriteLine(result);

        }


        public void Part2()
        {
            string filePath = "Input/Day6Example.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day6 Part2");

            string[] timeParts = dataList[0].Split(":");
            string[] timeStrings = timeParts[1].Trim().Replace(" ", "").Split(" ");
            List<int> times = new List<int>();

            foreach (string timeString in timeStrings)
            {
                if (timeString.Length > 0)
                {
                    times.Add(int.Parse(timeString));
                }
            }

            string[] distanceParts = dataList[1].Split(":");
            string[] distanceStrings = distanceParts[1].Trim().Replace(" ", "").Split(" ");
            List < double> distances = new List<double>();

            foreach (string distanceString in distanceStrings)
            {
                if (distanceString.Length > 0)
                {
                    distances.Add(double.Parse(distanceString));
                }
            }

            List<int> results = new List<int>();
            for (int i = 0; i < times.Count; i++)
            {
                int time = times[i];
                double distance = distances[i];

                int max = (int)Math.Ceiling(((-time - Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / -2) - 1);
                int min = (int)Math.Floor(((-time + Math.Sqrt(Math.Pow(time, 2) - 4 * distance)) / -2) + 1);

                int timeDifference = max - min + 1;

                results.Add(timeDifference);
            }

            int result = 1;
            foreach (int number in results)
            {
                result *= number;
            }
            Console.WriteLine(result);
        }            
    }
}
