using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day9
    {     

        public int GoDeeper(int[] numbers)
        {
            HashSet<int> hash = new HashSet<int>(numbers);
            if (hash.Count == 1)
            {
                return numbers[0];
            }

            List<int> nextIterList = new List<int>();
            for (int i = 1; i<numbers.Length; i++)
            {
                nextIterList.Add(numbers[i] - numbers[i-1]);
            }
            int[] nextIterNumbers = nextIterList.ToArray();

            return numbers[numbers.Length - 1] + GoDeeper(nextIterNumbers);
        }

        public void Part1()
        {
            string filePath = "Input/Day9.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day9 Part1");

            List<int> results = new List<int>();

            foreach (string line in dataList)
            {
                int[] numbers = Array.ConvertAll(line.Split(' '), int.Parse);
                results.Add(GoDeeper(numbers));
            }

            double result = 0;
            foreach(int i in results)
            {
                result += i;
            }
            Console.WriteLine(result);
        }


        public void Part2()
        {
            string filePath = "Input/Day9.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day9 Part1");

            List<int> results = new List<int>();

            foreach (string line in dataList)
            {
                int[] numbers = Array.ConvertAll(line.Split(' '), int.Parse);
                Array.Reverse(numbers);
                results.Add(GoDeeper(numbers));
            }

            double result = 0;
            foreach (int i in results)
            {
                result += i;
            }
            Console.WriteLine(result);
        }            
    }
}
