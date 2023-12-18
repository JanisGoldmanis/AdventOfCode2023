using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day10
    {
        public void Part1()
        {
            string filePath = "Input/Day10.txt";
            var dataList = File.ReadAllLines(filePath);

            List<char[]> maze= new List<char[]>();
            
            foreach (string line in dataList)
            {
                maze.Add(line.ToCharArray());
            }

            Console.WriteLine("Starting Day10 Part1");

            Dictionary<string, int[]> directionValue = new Dictionary<string, int[]>();
            directionValue["N"] = new int[] { -1, 0 };
            directionValue["S"] = new int[] { 1, 0 };
            directionValue["W"] = new int[] { 0, -1 };
            directionValue["E"] = new int[] { 0, 1 };

            Dictionary<char, Dictionary<string, string>> entryExit = new Dictionary<char, Dictionary<string, string>>();

            entryExit['|'] = new Dictionary<string, string>
            {
                {"N", "N"},
                {"S", "S"}
            };
            entryExit['-'] = new Dictionary<string, string>
            {
                {"W", "W"},
                {"E", "E"}
            };
            entryExit['L'] = new Dictionary<string, string>
            {
                {"S", "E"},
                {"W", "N"}
            };
            entryExit['J'] = new Dictionary<string, string>
            {
                {"E", "N"},
                {"S", "W"}
            };
            entryExit['7'] = new Dictionary<string, string>
            {
                {"E", "S"},
                {"N", "W"}
            };
            entryExit['F'] = new Dictionary<string, string>
            {
                {"W", "S"},
                {"N", "E"}
            };

            int[] firstDirectionCoordinate = { 43, 25 };
            string firstDirection = "E";
            int[] secondDirectionCoordinate = { 42, 24 };
            string secondDirection = "N";
            int steps = 1;

            while (true)
            {
                if (firstDirectionCoordinate[0] == secondDirectionCoordinate[0] && firstDirectionCoordinate[1] == secondDirectionCoordinate[1] )
                {
                    break;
                }
                steps++;

                int[] firstDirectionArray = directionValue[firstDirection];
                firstDirectionCoordinate[0] = firstDirectionCoordinate[0] + firstDirectionArray[0];
                firstDirectionCoordinate[1] = firstDirectionCoordinate[1] + firstDirectionArray[1];
                char firstDirectionChar = maze[firstDirectionCoordinate[0]][firstDirectionCoordinate[1]];
                Dictionary<string, string> entryExitPair = entryExit[firstDirectionChar];
                firstDirection = entryExitPair[firstDirection];

                int[] secondDirectionArray = directionValue[secondDirection];
                secondDirectionCoordinate[0] = secondDirectionCoordinate[0] + secondDirectionArray[0];
                secondDirectionCoordinate[1] = secondDirectionCoordinate[1] + secondDirectionArray[1];
                char secondDirectionChar = maze[secondDirectionCoordinate[0]][secondDirectionCoordinate[1]];
                entryExitPair = entryExit[secondDirectionChar];
                secondDirection = entryExitPair[secondDirection];
            }
            Console.WriteLine(steps);
        }


        public void Part2()
        {
            string filePath = "Input/Day10.txt";
            var dataList = File.ReadAllLines(filePath);

            List<char[]> maze = new List<char[]>();
            List<List<List<string>>> insideDirection = new List<List<List<string>>>();

            foreach (string line in dataList)
            {
                maze.Add(line.ToCharArray());
                List<List<string>> lineStrings = new List<List<string>>();

                insideDirection.Add(lineStrings);
                
                for (int i = 0; i< line.Length; i++)
                {
                    lineStrings.Add(new List<string>());
                }
            }

            Console.WriteLine("Starting Day10 Part2");

            Dictionary<string, int[]> directionValue = new Dictionary<string, int[]>();
            directionValue["N"] = new int[] { -1, 0 };
            directionValue["S"] = new int[] { 1, 0 };
            directionValue["W"] = new int[] { 0, -1 };
            directionValue["E"] = new int[] { 0, 1 };
            directionValue["SE"] = new int[] { 1, 1 };
            directionValue["SW"] = new int[] { 1, -1 };
            directionValue["NW"] = new int[] { -1, -1 };
            directionValue["NE"] = new int[] { -1, 1 };

            Dictionary<char, Dictionary<string, string>> entryExit = new Dictionary<char, Dictionary<string, string>>();

            entryExit['|'] = new Dictionary<string, string>
            {
                {"N", "N"},
                {"S", "S"}
            };
            entryExit['-'] = new Dictionary<string, string>
            {
                {"W", "W"},
                {"E", "E"}
            };
            entryExit['L'] = new Dictionary<string, string>
            {
                {"S", "E"},
                {"W", "N"}
            };
            entryExit['J'] = new Dictionary<string, string>
            {
                {"E", "N"},
                {"S", "W"}
            };
            entryExit['7'] = new Dictionary<string, string>
            {
                {"E", "S"},
                {"N", "W"}
            };
            entryExit['F'] = new Dictionary<string, string>
            {
                {"W", "S"},
                {"N", "E"}
            };

            Dictionary<char, Dictionary<string, string>> insideOutDefinition = new Dictionary<char, Dictionary<string, string>>();

            // | - L J 7 F
            insideOutDefinition['|'] = new Dictionary<string, string>
            {
                {"N,NE", "NE,SE"},
                {"N,NW", "NW,SW"},
                {"S,SW", "NW,SW"},
                {"S,SE", "NE,SE"}
            };
            insideOutDefinition['-'] = new Dictionary<string, string>
            {
                {"W,NW", "NW,NE"},
                {"W,SW", "SW,SE"},
                {"E,SE", "SW,SE"},
                {"E,NE", "NW,NE"}
            };
            insideOutDefinition['L'] = new Dictionary<string, string>
            {
                {"S,SE", "NE"},
                {"S,SW", "NW,SW,SE"},
                {"W,NW", "NE"},
                {"W,SW", "NW,SW,SE"}
            };
            insideOutDefinition['J'] = new Dictionary<string, string>
            {
                {"S,SE", "NE,SE,SW"},
                {"S,SW", "NW"},
                {"E,NE", "NW"},
                {"E,SE", "SW,SE,NE"}
            };
            insideOutDefinition['7'] = new Dictionary<string, string>
            {
                {"E,NE", "NW,NE,SE"},
                {"E,SE", "SW"},
                {"N,NW", "SW"},
                {"N,NE", "NW,NE,SE"}
            };
            insideOutDefinition['F'] = new Dictionary<string, string>
            {
                {"N,NE", "SE"},
                {"N,NW", "SW,NW,NE"},
                {"W,SW", "SE"},
                {"W,NW", "NE,NW,SW"}
            };

            int[] firstDirectionCoordinate = { 43, 25 };
            string firstDirection = "E";
            int[] secondDirectionCoordinate = { 42, 24 };
            string secondDirection = "N";

            insideDirection[43][25].Add("NE");
            insideDirection[42][24].Add("NE");

            maze[42][25] = 'P';
            insideDirection[42][25].Add("NE");
            insideDirection[42][25].Add("NW");
            insideDirection[42][25].Add("SE");

            int steps = 1;

            while (true)
            {
                if (firstDirectionCoordinate[0] == secondDirectionCoordinate[0] && firstDirectionCoordinate[1] == secondDirectionCoordinate[1])
                {
                    break;
                }
                steps++;

                maze[firstDirectionCoordinate[0]][firstDirectionCoordinate[1]] = 'P';
                maze[secondDirectionCoordinate[0]][secondDirectionCoordinate[1]] = 'P';

                //Inside Directions
                List<string> previousFirstInsideDirections = insideDirection[firstDirectionCoordinate[0]][firstDirectionCoordinate[1]];
                List<string> previousSecondInsideDirections = insideDirection[secondDirectionCoordinate[0]][secondDirectionCoordinate[1]];

                int[] firstDirectionArray = directionValue[firstDirection];
                firstDirectionCoordinate[0] = firstDirectionCoordinate[0] + firstDirectionArray[0];
                firstDirectionCoordinate[1] = firstDirectionCoordinate[1] + firstDirectionArray[1];
                char firstDirectionChar = maze[firstDirectionCoordinate[0]][firstDirectionCoordinate[1]];
                Dictionary<string, string> entryExitPair = entryExit[firstDirectionChar];

                //Inside Directions
                char firstShape = maze[firstDirectionCoordinate[0]][firstDirectionCoordinate[1]];
                Dictionary<string, string> firstDictionary = insideOutDefinition[firstShape];

                string[] firstKeys = firstDictionary.Keys.ToArray();

                bool firstFlag = false;

                foreach (string key in firstKeys)
                {
                    string[] keyParts = key.Split(',');

                    if (keyParts.Contains(firstDirection))
                    {
                        foreach(string previousDirection in previousFirstInsideDirections)
                        {
                            if (keyParts.Contains(previousDirection))
                            {
                                string[] newDirections = firstDictionary[key].Split(",").ToArray();
                                List<string> nextFirstInsideDirections = insideDirection[firstDirectionCoordinate[0]][firstDirectionCoordinate[1]];

                                foreach(string newDirection in newDirections)
                                {
                                    nextFirstInsideDirections.Add(newDirection);
                                } 
                                firstFlag = true;
                                break;
                            }
                        }
                    }
                    if (firstFlag)
                    {
                        break;
                    }
                }
                firstDirection = entryExitPair[firstDirection];

                int[] secondDirectionArray = directionValue[secondDirection];
                secondDirectionCoordinate[0] = secondDirectionCoordinate[0] + secondDirectionArray[0];
                secondDirectionCoordinate[1] = secondDirectionCoordinate[1] + secondDirectionArray[1];
                char secondDirectionChar = maze[secondDirectionCoordinate[0]][secondDirectionCoordinate[1]];
                entryExitPair = entryExit[secondDirectionChar];

                // Inside Directions
                char secondShape = maze[secondDirectionCoordinate[0]][secondDirectionCoordinate[1]];
                Dictionary<string, string> secondDictionary = insideOutDefinition[secondShape];


                string[] secondKeys = secondDictionary.Keys.ToArray();

                bool secondFlag = false;

                foreach (string key in secondKeys)
                {
                    string[] keyParts = key.Split(',');

                    if (keyParts.Contains(secondDirection))
                    {
                        foreach (string previousDirection in previousSecondInsideDirections)
                        {
                            if (keyParts.Contains(previousDirection))
                            {
                                string[] newDirections = secondDictionary[key].Split(",").ToArray();
                                List<string> nextSecondInsideDirections = insideDirection[secondDirectionCoordinate[0]][secondDirectionCoordinate[1]];

                                foreach (string newDirection in newDirections)
                                {
                                    nextSecondInsideDirections.Add(newDirection);
                                }
                                secondFlag = true;
                                break;
                            }
                        }
                    }
                    if (secondFlag)
                    {
                        break;
                    }
                }





                secondDirection = entryExitPair[secondDirection];
            }
            Console.WriteLine(steps);

            //foreach (char[] line in maze)
            //{
            //    foreach (char c in line)
            //    {
            //        Console.Write($"{c},");

            //    }
            //    Console.WriteLine();
            //}

            int enclosedSpaces = 0;

            while (true)
            {
                int changed = 0;

                for (int row = 0; row<maze.Count; row++)
                {
                    for (int column = 0; column < maze[0].Length; column++)
                    {
                        if (maze[row][column] != 'P' && maze[row][column] != '0')
                        {
                            if (row == 0 || row == 139 || column == 0 || column == 139)
                            {
                                maze[row][column] = '0';
                                changed++;
                                continue;
                            }
                            else
                            {
                                foreach (string direction in new string[] { "N", "S", "W", "E" , "NE", "NW", "SE", "SW"})
                                {
                                    int[] adjustment = directionValue[direction];
                                    char checkChar = maze[row + adjustment[0]][column + adjustment[1]];
                                    if (checkChar == '0')
                                    {
                                        maze[row][column] = '0';
                                        changed++;
                                        continue;
                                    }
                                }
                            }
                        }                        
                    }
                }
                //Console.WriteLine(changed);
                if (changed == 0)
                {
                    break;
                }
            }


            for (int row = 0; row < maze.Count; row++)
            {
                for (int column = 0; column < maze[0].Length; column++)
                {
                    if (maze[row][column] != 'P' && maze[row][column] != '0')
                    {
                        int i = 0;
                        while (true)
                        {
                            i++;
                            if (maze[row][column+i] == '0')
                            {
                                maze[row][column] = '0';
                            }
                            if (maze[row][column+i] == 'P')
                            {
                                List<string> thisDirection = insideDirection[row][column + i];
                                if (thisDirection.Contains("SW") || thisDirection.Contains("NW"))
                                {
                                    maze[row][column] = '1';
                                }
                                else
                                {
                                    maze[row][column] = 'Z';
                                }
                                break;
                            }
                        }
                    }
                }
            }



            foreach (char[] line in maze)
            {
                foreach (char c in line)
                {
                    if (c == '1')
                    {
                        enclosedSpaces++;
                    }
                }
            }

            Console.WriteLine(enclosedSpaces);

            foreach (char[] line in maze)
            {
                foreach (char c in line)
                {
                    Console.Write($"{c},");

                }
                Console.WriteLine();
            }
        }            
    }
}
