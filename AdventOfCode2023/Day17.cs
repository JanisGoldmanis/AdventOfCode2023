using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day17
    {     
        public class Location
        {
            public Tuple<int, int> Coordinate { get; set; }
            public char Direction { get; set; }
            public int SameDirectionCount { get; set; }

            public string PreviousDirection { get; set; }



        }

        public int[][] GetMaze(string[] input)
        {
            int rows = input.Length;
            int cols = input[0].Length;

            int[][] maze = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                maze[i] = new int[cols];
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    maze[i][j] = int.Parse(input[i][j].ToString());
                }
            }
            return maze;
        }


        public void Part1()
        {
            Console.WriteLine("Starting Day17 Part1");
            string filePath = "Input/Day17E.txt";
            var dataList = File.ReadAllLines(filePath);

            int[][] maze = GetMaze(dataList);

            List<Location> locations = new List<Location>();

            locations.Add(new Location { Coordinate =  new Tuple<int, int>(0,0) });

        }


        public void Part2()
        {
            Console.WriteLine("Starting Day17 Part2");
            string filePath = "Input/Day17E.txt";
            var dataList = File.ReadAllLines(filePath);            
        }            
    }
}
