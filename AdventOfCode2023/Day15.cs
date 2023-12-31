using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day15
    {     
        public int HashChar(char c, int currentValue)
        {
            int ascii = (int)c;
            currentValue += ascii;
            currentValue *= 17;
            currentValue = currentValue % 256;
            return currentValue;
        }

        public int HashInstruction(string input)
        {
            int hashValue = 0;
            foreach (char c in input)
            {
                hashValue = HashChar(c, hashValue);  
            }
            return hashValue;
        }


        public void Part1()
        {
            Console.WriteLine("Starting Day15 Part1");
            string filePath = "Input/Day15.txt";
            var dataList = File.ReadAllLines(filePath);

            string[] input = dataList[0].Split(",");
            int result = 0;

            foreach (string hash in input)
            {
                int currentValue = 0;
                foreach (char c in hash)
                {
                    currentValue = HashChar(c, currentValue);
                }
                result += currentValue;
            }
            Console.WriteLine(result);                                    
        }



        public void Part2()
        {
            Console.WriteLine("Starting Day15 Part2");
            string filePath = "Input/Day15.txt";
            var dataList = File.ReadAllLines(filePath);

            string[] input = dataList[0].Split(",");
            int result = 0;

            List<string>[] hashmap = new List<string>[256];
            for (int i = 0; i< hashmap.Length; i++)
            {
                hashmap[i] = new List<string>();
            }

            foreach (string line in input)
            {
                if (line.Contains('='))
                {
                    string hash = line.Substring(0, line.IndexOf('='));
                    string lens = line.Substring(line.IndexOf("=") + 1);
                    int hashValue = HashInstruction(hash);

                    bool changed = false;
                    for (int i = 0; i < hashmap[hashValue].Count; i++)
                    {
                        string hashLine = hashmap[hashValue][i];
                        if (hashLine.Contains(hash))
                        {
                            hashmap[hashValue][i] = hash + " " + lens;
                            changed = true;
                            break;
                        }
                    }
                    if (!changed)
                    {
                        hashmap[hashValue].Add(hash+ " " + lens);
                    }
                }
                if (line.Contains('-')) 
                {
                    string hash = line.Substring(0, line.IndexOf('-'));
                    int hashValue = HashInstruction(hash);

                    for (int i = 0; i < hashmap[hashValue].Count; i++)
                    {
                        string hashLine = hashmap[hashValue][i];
                        if (hashLine.Contains(hash))
                        {
                            hashmap[hashValue].RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            for (int i = 1; i<=hashmap.Length; i++) 
            {
                List<string> box = hashmap[i - 1];

                for (int j = 1; j <= box.Count; j++)
                {
                    result += i * j * int.Parse(box[j-1].Split(' ')[1]);
                }
            }
            Console.WriteLine(result);
        }            
    }
}
