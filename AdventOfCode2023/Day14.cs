using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day14
    {     
        public List<List<char>> TransposeMatrix(List<List<char>> matrix)
        {
            List<List<char>> result = new List<List<char>>();

            int lastIndex = matrix[0].Count - 1;

            for (int column = lastIndex; column >= 0; column--)
            {
                List<char> newColumn = new List<char>();

                for (int row = 0; row < matrix.Count; row++)
                {
                    newColumn.Add(matrix[row][column]);
                }
                result.Add(newColumn);
            }
            return result;
        }

        public List<List<char>> ReadFile(string filename)
        {
            List<List<char>> input = new List<List<char>>();
            var dataList = File.ReadAllLines(filename);
            foreach (var line in dataList)
            {
                input.Add(line.ToCharArray().ToList());
            }
            return input;
        }


        public void MoveForward(List<char> line)
        {
            bool notChanged = true;

            for (int i = 1; i<line.Count; i++)
            {
                char c = line[i];
                if (c == 'O')
                {
                    for (int moveBack = 1; moveBack <= i; moveBack++)
                    {
                        int index = i - moveBack;
                        char c2 = line[index];

                        if (line[0] == '.' && index == 0)
                        {
                            line[i] = '.';
                            line[i - moveBack] = 'O';
                            notChanged = false;
                            break;
                        }


                        if (c2 == '#' || c2 == 'O')
                        {
                            if (moveBack > 1)
                            {
                                line[i] = '.';
                                line[i - moveBack + 1] = 'O';
                                notChanged = false;
                                break;
                            }
                            else
                            {
                                if (moveBack == 1)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }            
        }


        public int LoadOnNorth(List<List<char>> matrix)
        {
            int result = 0;

            foreach (List<char> column in matrix)
            {
                int count = column.Count;
                for(int i = 0; i<count; i++)
                {
                    if (column[i] == 'O')
                    {
                        result += count - i;
                    }
                }
            }
            return result;
        }

        public void Part1()
        {
            Console.WriteLine("Starting Day14 Part1");
            string filePath = "Input/Day14.txt";

            List<List<char>> input = ReadFile(filePath);     
            
            input = TransposeMatrix(input);

            foreach(List<char> line in input)
            {
                MoveForward(line);
            }

            int result = LoadOnNorth(input);
        }


        public void PrintMatrix(List<List<char>> matrix)
        {
            foreach(List<char> line in matrix)
            {
                foreach (char c in line)
                {
                    Console.Write(c);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public string StringifyMatrix(List<List<char>> matrix) 
        {
            string result = "";

            foreach (List<char> line in matrix)
            {
                foreach (char c in line)
                {
                    result += c;
                }
            }

            return result;
        }


        public (string, List<List<char>>) Cycle(List<List<char>> matrix)
        {
            //PrintMatrix(matrix);

            //North

            //Console.WriteLine("NORTH\n");

            matrix = TransposeMatrix(matrix);
            
            //Console.WriteLine("Rotate North to Left");
            //PrintMatrix(matrix);
            foreach (List<char> line in matrix)
            {
                MoveForward(line);
            }

            int loadNorth = LoadOnNorth(matrix);

            //Console.WriteLine("Move Balls Left");
            //PrintMatrix(matrix);

            matrix = TransposeMatrix(matrix);
            matrix = TransposeMatrix(matrix);
            matrix = TransposeMatrix(matrix);

            //Console.WriteLine("Rotate North Back UP");
            //PrintMatrix(matrix);

            //West
            //Console.WriteLine("WEST\n");

            foreach (List<char> line in matrix)
            {
                MoveForward(line);
            }

            int loadWest = LoadOnNorth(matrix);

            //Console.WriteLine("Move Balls Left");
            //PrintMatrix(matrix);

            // South
            //Console.WriteLine("SOUTH\n");
            matrix = TransposeMatrix(matrix);
            matrix = TransposeMatrix(matrix);
            matrix = TransposeMatrix(matrix);

            //Console.WriteLine("Rotate South to Left");
            //PrintMatrix(matrix);

            foreach (List<char> line in matrix)
            {
                MoveForward(line);
            }

            //Console.WriteLine("Move Balls Left");
            //PrintMatrix(matrix);

            int loadSouth = LoadOnNorth(matrix);

            matrix = TransposeMatrix(matrix);

            //Console.WriteLine("Rotate North Back UP");

            //PrintMatrix(matrix);

            // East
            //Console.WriteLine("EAST");
            matrix = TransposeMatrix(matrix);
            matrix = TransposeMatrix(matrix);
            foreach (List<char> line in matrix)
            {
                MoveForward(line);
            }

            int loadEast = LoadOnNorth(matrix);

            matrix = TransposeMatrix(matrix);
            matrix = TransposeMatrix(matrix);

            //PrintMatrix(matrix);


            return (new string($"{loadNorth}-{loadWest}-{loadSouth}-{loadEast}"), matrix);
            //return StringifyMatrix(matrix);
        }

        public void Part2()
        {
            string filePath = "Input/Day14.txt";
            Console.WriteLine("Starting Day14 Part2");

            List<List<char>> input = ReadFile(filePath);

            Dictionary<string, int> cycleDictionary = new Dictionary<string, int>();

            int cycle = 0;
            int ogCycle = 0;

            while (true)
            {
                cycle++;
                Console.WriteLine($"Cycle {cycle}");

                (string iteration, input) = Cycle(input);

                if (cycleDictionary.ContainsKey(iteration))
                {
                    ogCycle = cycleDictionary[iteration];
                    break;
                }
                else
                {
                    cycleDictionary[iteration] = cycle;
                }
            }

            Console.WriteLine($"Cycle {cycle} was final cycle");
            Console.WriteLine($"It repeats from {ogCycle}");

            double iterations = 1000000000;

            double difference = iterations - ogCycle + 1;
            int cycleLength = cycle - ogCycle;

            double mod = (difference % (double) cycleLength);

            int i = 1;

            while (i<mod)
            {
                Console.WriteLine($"Cycle {cycle}");

                (string iteration, input) = Cycle(input);

                i++;
            }

            input = TransposeMatrix(input);

            int loadNorth = LoadOnNorth(input);

            Console.WriteLine(loadNorth);

        }            
    }
}
