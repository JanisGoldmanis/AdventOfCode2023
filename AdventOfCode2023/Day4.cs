using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day4
    {     
        public void Part1()
        {
            string filePath = "Input/Day4.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day4 Part1");

            int total_sum = 0;

            foreach (string line in dataList)
            {
                string[] numbers = line.Split(':')[1].Split('|');
                string[] winningNumbersString = numbers[0].Trim().Split(' ');
                string[] cardNumbersString = numbers[1].Trim().Split(' ');

                List<int> winningNumbers = new List<int>();
                List<int> cardNumbers = new List<int>();

                foreach (string number in winningNumbersString)
                {
                    if (number.Length > 0)
                    {
                        winningNumbers.Add(int.Parse(number));
                    }
                }

                foreach (string number in cardNumbersString)
                {
                    if (number.Length > 0)
                    {
                        cardNumbers.Add(int.Parse(number));
                    }
                }

                int ticket_sum = 0;

                foreach (int number in cardNumbers)
                {
                    if (winningNumbers.Contains(number))
                    {
                        if (ticket_sum == 0)
                        {
                            ticket_sum += 1;
                        }
                        else
                        {
                            ticket_sum *= 2;
                        }
                    }
                }

                total_sum += ticket_sum;

            }
            Console.WriteLine(total_sum);            
        }


        public void Part2()
        {
            string filePath = "Input/Day4.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day4 Part2");

            int total_sum = 0;

            List<int> ticketCounts = Enumerable.Repeat(1, dataList.Length).ToList();

            for(int i = 0; i<dataList.Length; i++)
            {
                int ticketCount = ticketCounts[i];

                string line = dataList[i];
                string[] numbers = line.Split(':')[1].Split('|');
                string[] winningNumbersString = numbers[0].Trim().Split(' ');
                string[] cardNumbersString = numbers[1].Trim().Split(' ');

                List<int> winningNumbers = new List<int>();
                List<int> cardNumbers = new List<int>();

                foreach (string number in winningNumbersString)
                {
                    if (number.Length > 0)
                    {
                        winningNumbers.Add(int.Parse(number));
                    }
                }

                foreach (string number in cardNumbersString)
                {
                    if (number.Length > 0)
                    {
                        cardNumbers.Add(int.Parse(number));
                    }
                }

                int ticket_sum = 0;
                foreach (int number in cardNumbers)
                {
                    if (winningNumbers.Contains(number))
                    {
                        ticket_sum +=1; // Aizstāts no *=2
                    }
                }

                for (int j = 0; j<ticket_sum; j++)
                {
                    ticketCounts[i+ 1 + j] += ticketCount;
                }
            }
            Console.WriteLine(ticketCounts.Sum());
        }            
    }
}
