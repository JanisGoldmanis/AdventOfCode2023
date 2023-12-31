using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day12
    {
        public class Line
        {
            public int[] Sizes { get; set; }
            public string Bar { get; set; }                 
            public List<int[]> permutations { get; set; }
        }

        static List<List<int>> GeneratePermutations(int[] numbers)
        {
            List<List<int>> result = new List<List<int>>();
            GeneratePermutationsHelper(numbers, 0, result);
            return result;
        }

        static void GeneratePermutationsHelper(int[] numbers, int currentIndex, List<List<int>> result)
        {
            if (currentIndex == numbers.Length - 1)
            {
                result.Add(new List<int>(numbers));
                return;
            }

            for (int i = currentIndex; i < numbers.Length; i++)
            {
                Swap(numbers, currentIndex, i);
                GeneratePermutationsHelper(numbers, currentIndex + 1, result);
                Swap(numbers, currentIndex, i); // Backtrack
            }
        }

        static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        static bool VerifyBar(string originalBar, string newBar)
        {
            for (int i = 0; i < Math.Min(originalBar.Length, newBar.Length); i++)
            {
                if (originalBar[i] == '?')
                {
                    continue;
                }
                if (originalBar[i] != newBar[i])
                {
                    return false;
                }
            }
            return true;
        }


        static int ValidRecursions(int[] sizes, int level, string originalBar, string oldTempBar)
        {
            int result = 0;

            string newBar = new string(oldTempBar);

            string spaces = "";
            for (int i = 0; i <= level; i++)
            {
                spaces += "    ";
            }

            Console.Write(spaces + "Sizes: ");
            foreach (int size in sizes)
            {
                Console.Write($"{size}   ");
            }
            Console.WriteLine();
            Console.WriteLine($"{spaces}{originalBar}");
            Console.WriteLine($"{spaces}{newBar}");

            if (sizes.Length > 0)
            {
                string checkBar = new string(newBar);


                for (int i = 0; i< originalBar.Length - newBar.Length; i++)
                {
                    checkBar += ".";
                }
                if (!VerifyBar(originalBar, checkBar))
                {
                    Console.WriteLine($"{spaces}+0");
                    return 0;
                }
            }            

            if (sizes.Length == 0)
            {
                int addDots = originalBar.Length - newBar.Length;
                for (int i =0; i < addDots; i++)
                {
                    newBar += ".";
                }


                if (VerifyBar(originalBar, newBar))
                {
                    Console.WriteLine($"{spaces}+1");
                    return 1;
                }
                Console.WriteLine($"{spaces}+0");
                return 0;
            }

            int nextSize = sizes[0];

            int totalLength = originalBar.Length;
            int currentBarLength = oldTempBar.Length;

            int necessaryToAllocate = 0;

            foreach (int size in sizes)
            {
                necessaryToAllocate += size + 1;
            }

            int oldTempBarLength = oldTempBar.Length;
            if (oldTempBar.Length == 0)
            {
                necessaryToAllocate -= 1;
            }

            int maxRange = totalLength - currentBarLength - necessaryToAllocate;


            // Try to place first size in all of the positions of first bar
            for (int i = 0; i <= maxRange; i++)
            {
                Console.WriteLine($"{spaces}Iteration {i}");


                string iterationBar = new string(oldTempBar);

                if (iterationBar.Length > 0)
                {
                    iterationBar += ".";
                }
                
                for (int j = 0; j<i; j++)
                {
                    iterationBar += ".";
                }

                for (int j = 0; j < nextSize; j++)
                {
                    iterationBar += "#";
                }                

                int[] sizesNext = new int[sizes.Length - 1];
                Array.Copy(sizes, 1, sizesNext, 0, sizes.Length - 1);
                result += ValidRecursions(sizesNext, level+1, originalBar, iterationBar);
            }
            Console.WriteLine($"{spaces}Returning {result}");
            return result;
        }

        static int ValidPermutation(int[] sizes, string bar)
        {
            int combinations = 0;
            //List<string> barSegments = bar.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList();

            combinations += ValidRecursions(sizes, 0, bar, "");

            return combinations;
        }


        public void Part1()
        {
            Console.WriteLine("Starting Day12 Part1");
            string filePath = "Input/Day12Example.txt";
            var dataList = File.ReadAllLines(filePath); 

            List<Line> lines = new List<Line>();

            foreach(string line in dataList)
            {
                string[] parts = line.Split(' ');
                int[] sizes = Array.ConvertAll(parts[1].Split(',').ToArray(), int.Parse);
                

                List<List<int>> permutations = GeneratePermutations(sizes);
                List<string> permutationStrings = new List<string>();

                foreach (var permutation in permutations)
                {
                    permutationStrings.Add(string.Join(", ", permutation));
                }
                permutationStrings = permutationStrings.Distinct().ToList();

                List<int[]> permutationInt = new List<int[]>();

                foreach(string permutation in permutationStrings)
                {
                    permutationInt.Add(Array.ConvertAll(permutation.Split(",").ToArray(), int.Parse));
                }
                lines.Add(new Line { Bar = parts[0], Sizes = sizes, permutations = permutationInt });

            }

            int combinations = 0;

            foreach(Line line in lines)
            {
                Console.WriteLine("\nNew Line");
                string bar = line.Bar;

                List<int[]> permutations = line.permutations;

                //foreach (int[] permutation in permutations)
                //{

                Console.Write("Sizes in Line: ");
                foreach (int size in line.Sizes)
                {
                    Console.Write($"{size} ");
                }
                Console.WriteLine();

                int combinationsTemp = ValidPermutation(line.Sizes, bar);
                Console.WriteLine($"Line combinations: {combinationsTemp}");
                combinations += combinationsTemp;                    
                //}
            }

            Console.WriteLine($"Total combinations: {combinations}");
        }

        public class LinePart2
        {
            public List<int> Sizes { get; set; }
            public string Bar { get; set; }
        }


        public double Part2Recursion(string bar, List<int> sizes, string barBuildup, Dictionary<string, double> combinationResults)
        {
            string iteration = barBuildup + string.Join(", ", sizes);

            string iterationString = new string(barBuildup);

            // Normal recursive end conditions
            if (sizes.Count == 0)
            {
                int dotCount = bar.Length - barBuildup.Length;
                string checkBar = new string(iterationString);

                for (int i = 0; i < dotCount; i++)
                {
                    checkBar += ".";
                }

                if (VerifyBar(bar, checkBar))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }                
            }

            double result = 0;

            int necessaryAllocation = 0;
            for ( int i = 0; i < sizes.Count; i++)
            {
                necessaryAllocation += sizes[i] + 1;
            }
            if (barBuildup.Length == 0) 
            {
                necessaryAllocation -= 1;
            }

            int maxRange = bar.Length - barBuildup.Length - necessaryAllocation;

            for (int i = 0; i<=maxRange; i++)
            {
                string nextBarBuildup = new string(iterationString);

                if (barBuildup.Length > 0)
                {
                    nextBarBuildup += ".";
                }                

                for (int j = 0; j < i; j++)
                {
                    nextBarBuildup += ".";
                }


                int nextSize = sizes[0];

                List<int> nextIterationSizes = sizes.ToList();
                nextIterationSizes.RemoveAt(0);

                int leftOverBar = bar.Length - nextBarBuildup.Length - nextSize;
                string leftOverSizes = string.Join(", ", nextIterationSizes);

                string key = leftOverBar.ToString() + "-" + leftOverSizes;

                double iterationResult = 0;

                // Check if result is memoized
                if (combinationResults.ContainsKey(key))
                {
                    result += combinationResults[key];
                }

                else
                {
                    for (int j = 0; j < nextSize; j++)
                    {
                        nextBarBuildup += "#";
                    }

                    if (VerifyBar(bar, nextBarBuildup))
                    {
                        iterationResult = Part2Recursion(bar, nextIterationSizes, nextBarBuildup, combinationResults);
                        result += iterationResult;
                        combinationResults[key] = iterationResult;
                    }
                }

            }
            return result;
        }


        public void Part2()
        {
            Console.WriteLine("Starting Day12 Part2");

            string filePath = "Input/Day12Example.txt";
            var dataList = File.ReadAllLines(filePath);            

            List<LinePart2> lines = new List<LinePart2>();            

            double combinations = 0;

            foreach (string line in dataList)
            {
                string[] parts = line.Split(' ');

                string barIter = parts[0];
                string bar = parts[0];

                for (int i = 0; i < 4; i++)
                {
                    bar += "?" + barIter;
                }

                List<int> sizes = Array.ConvertAll(parts[1].Split(',').ToArray(), int.Parse).ToList();

                List<int> repeatedList = Enumerable.Range(1, 5)
                                           .SelectMany(_ => sizes)
                                           .ToList();

                lines.Add(new LinePart2 { Bar = bar, Sizes = repeatedList });
            }

            foreach (LinePart2 line in lines)
            {
                Console.WriteLine(line.Bar);
                Dictionary<string, double> combinationResults = new Dictionary<string, double>();
                combinations += Part2Recursion(line.Bar, line.Sizes, "", combinationResults);
            }

            Console.WriteLine(combinations);

        }            
    }
}
