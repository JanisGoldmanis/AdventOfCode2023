using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day11
    {
        public void Part1()
        {
            string filePath = "Input/Day11.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day11 Part1");

            List<int[]> galaxyCoordinates = new List<int[]>();

            for (int row = 0; row < dataList.Length; row++)
            {
                for (int column = 0; column < dataList[0].Length; column++)
                {
                    if (dataList[row][column] == '#')
                    {
                        galaxyCoordinates.Add(new int[] { row, column });
                    }
                }
            }

            // Check rows
            galaxyCoordinates = galaxyCoordinates.OrderBy(tuple => tuple[0]).ToList();

            int adjustment = 0;

            for (int i = 1; i < galaxyCoordinates.Count; i++)
            {
                int difference = galaxyCoordinates[i][0] - galaxyCoordinates[i - 1][0];
                galaxyCoordinates[i - 1][0] += adjustment;
                if (difference > 1)
                {
                    adjustment += difference - 1;
                }
            }
            galaxyCoordinates[galaxyCoordinates.Count - 1][0] += adjustment;

            // Check columns
            galaxyCoordinates = galaxyCoordinates.OrderBy(tuple => tuple[1]).ToList();

            adjustment = 0;

            for (int i = 1; i < galaxyCoordinates.Count; i++)
            {
                int difference = galaxyCoordinates[i][1] - galaxyCoordinates[i - 1][1];
                galaxyCoordinates[i - 1][1] += adjustment;
                if (difference > 1)
                {
                    adjustment += difference - 1;
                }
            }
            galaxyCoordinates[galaxyCoordinates.Count - 1][1] += adjustment;

            galaxyCoordinates = galaxyCoordinates
                .OrderBy(tuple => tuple[0])
                .ThenBy(tuple => tuple[1])
                .ToList();


            int distanceSum = 0;
            //Calculate distances
            while (galaxyCoordinates.Count > 1)
            {
                int[] galaxyCoordinate1 = galaxyCoordinates[0];
                galaxyCoordinates.RemoveAt(0);

                foreach(int[] galaxyCoordinate2 in galaxyCoordinates)
                {
                    int y = Math.Abs(galaxyCoordinate1[0] - galaxyCoordinate2[0]);
                    int x = Math.Abs(galaxyCoordinate1[1] - galaxyCoordinate2[1]);

                    distanceSum += x + y;
                }
            }
            Console.WriteLine(distanceSum);
        }


        public void Part2()
        {
            string filePath = "Input/Day11.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day11 Part2");

            List<double[]> galaxyCoordinates = new List<double[]>();

            for (int row = 0; row < dataList.Length; row++)
            {
                for (int column = 0; column < dataList[0].Length; column++)
                {
                    if (dataList[row][column] == '#')
                    {
                        galaxyCoordinates.Add(new double[] { row, column });
                    }
                }
            }

            List<double> emptyRows = new List<double>();
            galaxyCoordinates = galaxyCoordinates.OrderBy(tuple => tuple[0]).ToList();
            for (double i = 0; i < dataList.Length; i++)
            {
                bool found = false;
                foreach (double[] galaxyCoordinate in galaxyCoordinates)
                {
                    if (i == galaxyCoordinate[0])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    emptyRows.Add(i);
                }
            }

            List<double> emptyColumns = new List<double>();
            galaxyCoordinates = galaxyCoordinates.OrderBy(tuple => tuple[1]).ToList();
            for (double i = 0; i < dataList[0].Length; i++)
            {
                bool found = false;
                foreach (double[] galaxyCoordinate in galaxyCoordinates)
                {
                    if (i == galaxyCoordinate[1])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    emptyColumns.Add(i);
                }
            }

            double adjustment = 1000000;

            for (int i = 1; i < galaxyCoordinates.Count; i++)
            {
                double[] galaxyCoordinate = galaxyCoordinates[i];

                int expandingRowCounter = 0;
                foreach(double emptyRow in emptyRows)
                {
                    if (emptyRow < galaxyCoordinate[0])
                    {
                        expandingRowCounter += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                galaxyCoordinate[0] += (adjustment - 1) * expandingRowCounter;

                int expandingColumnCounter = 0;
                foreach(double emptyColumn in emptyColumns)
                {
                    if (emptyColumn < galaxyCoordinate[1])
                    {
                        expandingColumnCounter += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                galaxyCoordinate[1] += (adjustment - 1) * expandingColumnCounter;
            }

            galaxyCoordinates = galaxyCoordinates
                .OrderBy(tuple => tuple[0])
                .ThenBy(tuple => tuple[1])
                .ToList();

            double distanceSum = 0;

            while (galaxyCoordinates.Count > 1)
            {
                double[] galaxyCoordinate1 = galaxyCoordinates[0];
                galaxyCoordinates.RemoveAt(0);

                foreach (double[] galaxyCoordinate2 in galaxyCoordinates)
                {
                    double y = Math.Abs(galaxyCoordinate1[0] - galaxyCoordinate2[0]);
                    double x = Math.Abs(galaxyCoordinate1[1] - galaxyCoordinate2[1]);

                    distanceSum += x + y;
                }
            }
            Console.WriteLine(distanceSum);
        }            
    }
}
