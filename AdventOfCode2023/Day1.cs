using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;


namespace AdventOfCode2023
{
    internal class Day1
    {
        public void Part2()
        {

            string filePath = "Input/Day1.txt";
            string[] numberStrings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            static int GetFirstNumber(string input, string[] numberStrings)
            {             
                for (int i = 0; i < input.Length; i++)
                {
                    char character = input[i];
                    if (char.IsDigit(character))
                    {
                        return int.Parse(character.ToString());
                    }

                    for (int j = 0; j < 10; j++)
                    {
                        try
                        {
                            string word = numberStrings[j];
                            int wordLength = word.Length;
                            string subString = input.Substring(i, wordLength);
                            if (subString == word)
                            {
                                return j;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                return -1;
            }


            static int GetLastNumber(string input, string[] numberStrings)
            {
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    char character = input[i];
                    if (char.IsDigit(character))
                    {
                        return int.Parse(character.ToString());
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        try
                        {
                            string word = numberStrings[j];
                            int wordLength = word.Length;

                            string subString = input.Substring(i - wordLength + 1, wordLength);
                            if (subString == word)
                            {
                                return j;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                return -1;
            }


            static string[] GetFirstAndLastNumbers(string input, string[] numberStrings)
            {
                string firstNumber = Convert.ToString(GetFirstNumber(input, numberStrings));
                string lastNumber = Convert.ToString(GetLastNumber(input, numberStrings));
                return new string[] { firstNumber, lastNumber };
            }


            int total_sum = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] result = GetFirstAndLastNumbers(line, numberStrings);
                    string combinedString = string.Join("", result);
                    int number = int.Parse(combinedString);
                    total_sum += number;
                }
                Console.WriteLine(total_sum);
            }
        }
    }
}