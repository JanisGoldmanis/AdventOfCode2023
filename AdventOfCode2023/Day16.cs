using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day16
    {     
        public char[][] GetMaze(string[] input)
        {
            int rows = input.Length;
            int cols = input[0].Length;

            char[][] maze = new char[rows][];

            for (int i = 0; i < rows; i++)
            {
                maze[i] = new char[cols];
            }

            for (int i = 0; i<rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    maze[i][j] = input[i][j];   
                }
            }
            return maze;
        }

        public void PrintMaze(char[][] maze)
        {
            Console.WriteLine("Maze:");
            foreach (char[] line in maze)
            {
                foreach (char c in line)
                {
                    if (c == '\0')
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write(c);
                    }                    
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }


        public (int, int, string) SplitCoordinate(string coordinate)
        {
            string[] parts = coordinate.Split(";");

            string direction = parts[1];

            parts = parts[0].Split(",");

            int row = int.Parse(parts[0]);
            int column = int.Parse(parts[1]);

            return (row, column, direction);
        }

        public bool OutOfBoundsCheck(int nextRow, int nextColumn, int maxRow, int maxColumn)
        {
            if (nextRow < 0 || nextRow > maxRow)
            {
                return false;
            }
            if (nextColumn < 0 || nextColumn > maxColumn)
            {
                return false;
            }
            return true;
        }

        public void Part1()
        {
            Console.WriteLine("Starting Day16 Part1");
            string filePath = "Input/Day16.txt";
            var dataList = File.ReadAllLines(filePath);

            char[][] maze = GetMaze(dataList);

            char[][] visitedCoordinates = new char[maze.Length][];

            for (int i = 0; i < maze.Length; i++)
            {
                visitedCoordinates[i] = new char[maze[0].Length];
            }

            int maxRows = maze[0].Length - 1;
            int maxColumns = maze[1].Length - 1;

            HashSet<string> visited = new HashSet<string>();

            List<string> directions = new List<string> { "0,-1;E" };
            visited.Add("0,-1;E");
            visitedCoordinates[0][0] = '#';

            Dictionary<string, int[]> directionVector = new Dictionary<string, int[]>();
            directionVector["E"] = new int[] { 0, 1 };
            directionVector["W"] = new int[] { 0, -1 };
            directionVector["N"] = new int[] { -1, 0 };
            directionVector["S"] = new int[] { 1, 0 };

            Dictionary<string, List<string>> nextDirections = new Dictionary<string, List<string>>();
            nextDirections[".E"] = new List<string> { "E" };
            nextDirections[".W"] = new List<string> { "W" };
            nextDirections[".N"] = new List<string> { "N" };
            nextDirections[".S"] = new List<string> { "S" };

            nextDirections["|E"] = new List<string> { "S", "N" };
            nextDirections["|W"] = new List<string> { "S", "N" };
            nextDirections["|N"] = new List<string> { "N" };
            nextDirections["|S"] = new List<string> { "S" };

            nextDirections["-N"] = new List<string> { "E", "W" };
            nextDirections["-S"] = new List<string> { "E", "W" };
            nextDirections["-E"] = new List<string> { "E" };
            nextDirections["-W"] = new List<string> { "W" };

            nextDirections["\\N"] = new List<string> { "W" };
            nextDirections["\\E"] = new List<string> { "S" };
            nextDirections["\\S"] = new List<string> { "E" };
            nextDirections["\\W"] = new List<string> { "N" };

            nextDirections["/N"] = new List<string> { "E" };
            nextDirections["/W"] = new List<string> { "S" };
            nextDirections["/E"] = new List<string> { "N" };
            nextDirections["/S"] = new List<string> { "W" };

            while (directions.Count > 0)
            {
                string direction = directions[0];
                directions.RemoveAt(0);

                (int row, int column, string directionChar) = SplitCoordinate(direction);

                int[] directionIntVector = directionVector[directionChar];

                int nextRow = row + directionIntVector[0];
                int nextColumn = column + directionIntVector[1];

                bool notOutOfBounds = OutOfBoundsCheck(nextRow, nextColumn, maxRows, maxColumns);

                if (!notOutOfBounds)
                {
                    continue;
                }
                char nextChar = maze[nextRow][nextColumn];

                List<string> nextDirectionsStrings = nextDirections[nextChar + directionChar];

                foreach(string nextDirectionsString in nextDirectionsStrings)
                {
                    string nextDirectionFullString = $"{nextRow},{nextColumn};{nextDirectionsString}";

                    visitedCoordinates[nextRow][nextColumn] = '#';

                    if (visited.Contains(nextDirectionFullString)) 
                    {
                        continue;
                    }
                    visited.Add(nextDirectionFullString );
                    directions.Add(nextDirectionFullString);
                }
            }

            int result = 0;
            foreach (char[] row in visitedCoordinates)
            {
                foreach (char c in row)
                {
                    if (c == '#')
                    {
                        result++;
                    }
                }
            }

            Console.WriteLine(result);
        }


        public void Part2()
        {
            Console.WriteLine("Starting Day16 Part2");
            string filePath = "Input/Day16.txt";
            var dataList = File.ReadAllLines(filePath);

            char[][] maze = GetMaze(dataList);

            int maxRows = maze[0].Length - 1;
            int maxColumns = maze[1].Length - 1;

            List<string> iterations = new List<string>();
            for (int i = 0; i< maxRows; i++)
            {
                iterations.Add($"{i},-1;E");
                iterations.Add($"{i},{maxColumns + 1};W");
            }
            for (int i = 0; i<maxColumns; i++)
            {
                iterations.Add($"-1,{i};S");
                iterations.Add($"{maxRows + 1},{i};N");
            }

            Dictionary<string, int[]> directionVector = new Dictionary<string, int[]>();
            directionVector["E"] = new int[] { 0, 1 };
            directionVector["W"] = new int[] { 0, -1 };
            directionVector["N"] = new int[] { -1, 0 };
            directionVector["S"] = new int[] { 1, 0 };

            Dictionary<string, List<string>> nextDirections = new Dictionary<string, List<string>>();
            nextDirections[".E"] = new List<string> { "E" };
            nextDirections[".W"] = new List<string> { "W" };
            nextDirections[".N"] = new List<string> { "N" };
            nextDirections[".S"] = new List<string> { "S" };

            nextDirections["|E"] = new List<string> { "S", "N" };
            nextDirections["|W"] = new List<string> { "S", "N" };
            nextDirections["|N"] = new List<string> { "N" };
            nextDirections["|S"] = new List<string> { "S" };

            nextDirections["-N"] = new List<string> { "E", "W" };
            nextDirections["-S"] = new List<string> { "E", "W" };
            nextDirections["-E"] = new List<string> { "E" };
            nextDirections["-W"] = new List<string> { "W" };

            nextDirections["\\N"] = new List<string> { "W" };
            nextDirections["\\E"] = new List<string> { "S" };
            nextDirections["\\S"] = new List<string> { "E" };
            nextDirections["\\W"] = new List<string> { "N" };

            nextDirections["/N"] = new List<string> { "E" };
            nextDirections["/W"] = new List<string> { "S" };
            nextDirections["/E"] = new List<string> { "N" };
            nextDirections["/S"] = new List<string> { "W" };

            int maxResult = 0;

            foreach (string iteration in iterations)
            {
                HashSet<string> visited = new HashSet<string>();

                List<string> directions = new List<string> { iteration };
                visited.Add(iteration);

                char[][] visitedCoordinates = new char[maze.Length][];

                for (int i = 0; i < maze.Length; i++)
                {
                    visitedCoordinates[i] = new char[maze[0].Length];
                }




                while (directions.Count > 0)
                {
                    string direction = directions[0];
                    directions.RemoveAt(0);

                    (int row, int column, string directionChar) = SplitCoordinate(direction);

                    int[] directionIntVector = directionVector[directionChar];

                    int nextRow = row + directionIntVector[0];
                    int nextColumn = column + directionIntVector[1];

                    bool notOutOfBounds = OutOfBoundsCheck(nextRow, nextColumn, maxRows, maxColumns);

                    if (!notOutOfBounds)
                    {
                        continue;
                    }
                    char nextChar = maze[nextRow][nextColumn];

                    List<string> nextDirectionsStrings = nextDirections[nextChar + directionChar];

                    foreach (string nextDirectionsString in nextDirectionsStrings)
                    {
                        string nextDirectionFullString = $"{nextRow},{nextColumn};{nextDirectionsString}";

                        visitedCoordinates[nextRow][nextColumn] = '#';

                        if (visited.Contains(nextDirectionFullString))
                        {
                            continue;
                        }
                        visited.Add(nextDirectionFullString);
                        directions.Add(nextDirectionFullString);
                    }
                }

                int result = 0;
                foreach (char[] row in visitedCoordinates)
                {
                    foreach (char c in row)
                    {
                        if (c == '#')
                        {
                            result++;
                        }
                    }
                }
                Console.WriteLine(result);

                if (result > maxResult)
                {
                    maxResult = result;
                }
                //PrintMaze(visitedCoordinates);
            }
            Console.WriteLine(maxResult);
        }            
    }
}
