using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day3
    {
        public class AocNumber
        {
            public int Number { get; set; }
            public Tuple<int, int> StartCoordinate { get; set; }
        }

        public List<string> createCoordinateListToCheck(AocNumber number)
        {
            int row = number.StartCoordinate.Item1;
            int col = number.StartCoordinate.Item2;

            List<string> coordinatesToCheck = new List<string>();

            for (int i = -1; i< Convert.ToString(number.Number).Length + 1; i++)
            {
                for (int j = -1; j<2; j++)
                {
                    coordinatesToCheck.Add($"{row + j},{col + i}");
                }
            }
            return coordinatesToCheck;
        }


        public void Part1()
        {
            string filePath = "Input/Day3Full.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day3 Part1");

            //string concatenatedText = string.Concat(dataList);
            //var uniqueCharacters = concatenatedText.Distinct();

            char[] symbols = {'*', '=', '-', '+', '&', '#', '%', '/', '@', '$'};


            List<string> symbolCoordinates = new List<string>();
            List<AocNumber> numbers = new List<AocNumber>();


            int row = 0;
            foreach (string line in dataList)
            {
                int i = 0;
                StringBuilder stringBuilder = new StringBuilder("");

                while (i < line.Length)
                {
                    if (line[i] == '.')
                    {
                        i++;
                        continue;
                    }

                    if (symbols.Contains(line[i]))
                    {
                        symbolCoordinates.Add($"{row},{i}");
                        i++;
                        continue;
                    }

                    if (char.IsDigit(line[i]))
                    {
                        stringBuilder.Append(line[i]);

                        if (i < line.Length - 1) {
                            if (char.IsDigit(line[i + 1]))
                            {
                                i++;
                                continue;
                            }
                        }

                        string numberString = stringBuilder.ToString();
                        int number = int.Parse(numberString);
                        stringBuilder.Clear();
                        numbers.Add(new AocNumber
                        {
                            Number = number,
                            StartCoordinate = Tuple.Create(row, i - numberString.Length + 1)
                        });
                        i++;
                        continue;    
                    }
                    i++;
                }
                row++;
            }

            int total_sum = 0;            

            foreach (AocNumber number in numbers)
            {
                bool flag = false;
                List<string> coordinatesToCheck = createCoordinateListToCheck(number);

                foreach (string coordinates in coordinatesToCheck)
                {
                    if (flag)
                    {
                        break;
                    }
                    if (symbolCoordinates.Contains(coordinates))
                    {
                        total_sum += number.Number;
                        flag = true;                        
                    }
                }



            }
            Console.WriteLine(total_sum);
        }


        public void Part2()
        {
            string filePath = "Input/Day3Full.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day3 Part1");

            //string concatenatedText = string.Concat(dataList);
            //var uniqueCharacters = concatenatedText.Distinct();

            char[] symbols = {'*'};


            List<string> symbolCoordinates = new List<string>();
            List<AocNumber> numbers = new List<AocNumber>();


            int row = 0;
            foreach (string line in dataList)
            {
                int i = 0;
                StringBuilder stringBuilder = new StringBuilder("");

                while (i < line.Length)
                {
                    if (line[i] == '.')
                    {
                        i++;
                        continue;
                    }

                    if (symbols.Contains(line[i]))
                    {
                        symbolCoordinates.Add($"{row},{i}");
                        i++;
                        continue;
                    }

                    if (char.IsDigit(line[i]))
                    {
                        stringBuilder.Append(line[i]);

                        if (i < line.Length - 1)
                        {
                            if (char.IsDigit(line[i + 1]))
                            {
                                i++;
                                continue;
                            }
                        }

                        string numberString = stringBuilder.ToString();
                        int number = int.Parse(numberString);
                        stringBuilder.Clear();
                        numbers.Add(new AocNumber
                        {
                            Number = number,
                            StartCoordinate = Tuple.Create(row, i - numberString.Length + 1)
                        });
                        i++;
                        continue;
                    }
                    i++;
                }
                row++;
            }

            int total_sum = 0;

            foreach (string coordinate in symbolCoordinates)
            {
                List<AocNumber> coordinateAdjacentNumbers = new List<AocNumber>();

                foreach (AocNumber number in numbers)
                {
                    List<string> coordinatesToCheck = createCoordinateListToCheck(number);

                    if (coordinatesToCheck.Contains(coordinate))
                    {
                        coordinateAdjacentNumbers.Add(number);
                    }
                }                         

                if (coordinateAdjacentNumbers.Count == 2)
                {
                    total_sum += coordinateAdjacentNumbers[0].Number * coordinateAdjacentNumbers[1].Number;
                }

            }
            Console.WriteLine(total_sum);
        }

    }
}
