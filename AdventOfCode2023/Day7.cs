using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day7
    {     
        public void Part1()
        {
            string filePath = "Input/Day7.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day7 Part1");

            List<string> hands = new List<string>();
            List<int> bids = new List<int>();

            foreach (string line in dataList)
            {
                string[] parts = line.Split(" ");
                hands.Add(parts[0]);
                bids.Add(int.Parse(parts[1]));
            }

            char[] cardRankArr = { 'A','K','Q', 'J','T', '9', '8', '7', '6', '5', '4', '3', '2' };
            Array.Reverse(cardRankArr);


            string[] handRankArr = 
                { 
                "high card", 
                "one pair", 
                "two pair", 
                "three of a kind", 
                "full house", 
                "four of a kind", 
                "five of a kind" 
            };

            List<int> handRank = new List<int>();
            List<double> firstCardRank = new List<double>();  
            foreach(string hand in hands)
            {
                char[] cards = hand.ToArray();
                int[] cardRank = new int[cards.Length];
                for (int i = 0; i < cards.Length; i++) 
                {
                    cardRank[i] = Array.IndexOf(cardRankArr, cards[i]);
                }


                StringBuilder firstCardRankString = new StringBuilder();
                foreach( int rank in cardRank)
                {
                    firstCardRankString.Append(100 + rank);
                }
                double firstCardRankDouble = double.Parse(firstCardRankString.ToString());

                firstCardRank.Add(firstCardRankDouble);

                Array.Sort(cardRank);

                Dictionary<int, int> cardCount = new Dictionary<int, int>();

                for (int i = 0; i<cardRank.Length; i++)
                {
                    if (cardCount.ContainsKey(cardRank[i]))
                    {
                        // Increment the count if the card is already in the dictionary
                        cardCount[cardRank[i]]++;
                    }
                    else
                    {
                        // Add the card to the dictionary with a count of 1 if it's not present
                        cardCount[cardRank[i]] = 1;
                    }

                }

                List<int> cardCountList = new List<int>();
                foreach (var kvp in cardCount)
                {
                    cardCountList.Add(kvp.Value);
                }
                int[] cardCountArr = cardCountList.ToArray();
                Array.Sort(cardCountArr);
                Array.Reverse(cardCountArr);

                if (cardCount.Count == 5)
                {
                    handRank.Add(0);
                    continue;
                }
                if (cardCount.Count == 4)
                {
                    handRank.Add(1);
                    continue;
                }
                if (cardCountArr.Length == 3)
                {
                    if (cardCountArr[0] == 2)
                    {
                        handRank.Add(2);
                        continue;
                    }
                    else
                    {
                        handRank.Add(3);
                        continue;
                    }
                }
                if (cardCountArr.Length == 2)
                {
                    if (cardCountArr[0] == 3) 
                    {
                        handRank.Add(4);
                        continue;
                    }
                    else
                    {
                        handRank.Add(5);
                        continue;
                    }
                }
                if (cardCountArr.Length == 1)
                {
                    handRank.Add(6);
                    continue;
                }
            }

            var sortedBids = bids.Select((bid, index) => new { linqBID = bid, linqHandRank = handRank[index], linqFirstCardRank = firstCardRank[index] })
                .OrderBy(x => x.linqHandRank)
                .ThenBy(x => x.linqFirstCardRank)
                .Select(x => x.linqBID)
                .ToList();

            double result = 0;

            for (int i = 1; i<= sortedBids.Count; i++)
            {
                result += i * sortedBids[i-1];
            }
            Console.WriteLine(result);
        }


        public void Part2()
        {
            string filePath = "Input/Day7.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day7 Part1");

            List<string> hands = new List<string>();
            List<int> bids = new List<int>();

            foreach (string line in dataList)
            {
                string[] parts = line.Split(" ");
                hands.Add(parts[0]);
                bids.Add(int.Parse(parts[1]));
            }

            char[] cardRankArr = { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };
            Array.Reverse(cardRankArr);


            string[] handRankArr =
                {
                "high card",
                "one pair",
                "two pair",
                "three of a kind",
                "full house",
                "four of a kind",
                "five of a kind"
            };

            List<int> handRank = new List<int>();
            List<double> firstCardRank = new List<double>();
            foreach (string hand in hands)
            {
                char[] cards = hand.ToArray();
                int[] cardRank = new int[cards.Length];
                for (int i = 0; i < cards.Length; i++)
                {
                    cardRank[i] = Array.IndexOf(cardRankArr, cards[i]);
                }


                StringBuilder firstCardRankString = new StringBuilder();
                foreach (int rank in cardRank)
                {
                    firstCardRankString.Append(100 + rank);
                }
                double firstCardRankDouble = double.Parse(firstCardRankString.ToString());

                firstCardRank.Add(firstCardRankDouble);

                Array.Sort(cardRank);

                Dictionary<int, int> cardCount = new Dictionary<int, int>();

                List<int> cardRankList = cardRank.ToList();
                cardRankList.RemoveAll(item => item == 0);

                cardRank = cardRankList.ToArray();
               

                for (int i = 0; i < cardRank.Length; i++)
                {
                    if (cardCount.ContainsKey(cardRank[i]))
                    {
                        // Increment the count if the card is already in the dictionary
                        cardCount[cardRank[i]]++;
                    }
                    else
                    {
                        // Add the card to the dictionary with a count of 1 if it's not present
                        cardCount[cardRank[i]] = 1;
                    }

                }



                List<int> cardCountList = new List<int>();
                foreach (var kvp in cardCount)
                {
                    cardCountList.Add(kvp.Value);
                }
                int[] cardCountArr = cardCountList.ToArray();
                Array.Sort(cardCountArr);
                Array.Reverse(cardCountArr);

                if (cardCountList.Count == 0)
                {
                    cardCountArr = new int[] { 0};
                }

                cardCountArr[0] += 5 - cardRankList.Count;

                if (cardCount.Count == 5)
                {
                    //int maxDifference = 0;
                    //for (int i = 0; i<= cardRank.Length-1; i++)
                    //{
                    //    maxDifference = Math.Max(maxDifference, cardRank[i+1] - cardRank[i]);               
                    //}
                    handRank.Add(0);
                    continue;
                }
                if (cardCount.Count == 4)
                {
                    handRank.Add(1);
                    continue;
                }
                if (cardCountArr.Length == 3)
                {
                    if (cardCountArr[0] == 2)
                    {
                        handRank.Add(2);
                        continue;
                    }
                    else
                    {
                        handRank.Add(3);
                        continue;
                    }
                }
                if (cardCountArr.Length == 2)
                {
                    if (cardCountArr[0] == 3)
                    {
                        handRank.Add(4);
                        continue;
                    }
                    else
                    {
                        handRank.Add(5);
                        continue;
                    }
                }
                if (cardCountArr.Length == 1)
                {
                    handRank.Add(6);
                    continue;
                }
            }

            var sortedBids = bids.Select((bid, index) => new { linqBID = bid, linqHandRank = handRank[index], linqFirstCardRank = firstCardRank[index] })
                .OrderBy(x => x.linqHandRank)
                .ThenBy(x => x.linqFirstCardRank)
                .Select(x => x.linqBID)
                .ToList();

            double result = 0;

            for (int i = 1; i <= sortedBids.Count; i++)
            {
                result += i * sortedBids[i - 1];
            }
            Console.WriteLine(result);
        }            
    }
}
