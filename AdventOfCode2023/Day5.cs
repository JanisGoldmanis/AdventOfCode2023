using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day5
    {     
        public void Part1()
        {
            string filePath = "Input/Day5.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day5 Part1");

            double[] seeds = Array.ConvertAll(dataList[0].Split(':')[1].Trim().Split(' '), double.Parse);

            List<List<double[]>> input = new List<List<double[]>>();

            int counter = 3;
            while (counter < dataList.Length)
            {

                List<double[]> arrayOfLines = new List<double[]>();
                while (counter < dataList.Length)
                {
                    string line = dataList[counter];
                    if (line == "")
                    {                        
                        counter += 2;
                        break;
                    }
                    double[] numbers = Array.ConvertAll(dataList[counter].Split(' '), double.Parse);
                    arrayOfLines.Add(numbers);
                    counter++;
                }
                input.Add(arrayOfLines);
            }

            // Array of {x, y, z}
            // x - destination range start
            // y - source range start
            // z - range length

            List<double> results = new List<double>();

            for (int i = 0; i < seeds.Length; i++)
            {
                double seedResult = seeds[i];

                foreach (List<double[]> group in input)
                {
                    foreach (double[] line in group)
                    {
                        double destinationStart = line[0];
                        double sourceStart = line[1];
                        double range = line[2];

                        if (sourceStart + range > seedResult && seedResult >= sourceStart)
                        {
                            seedResult += destinationStart - sourceStart;
                            break;
                        }
                    }
                }
                results.Add(seedResult);                
            }
            double minimum = results.Min();
            Console.WriteLine(minimum);
        }


        public void Part2()
        {
            string filePath = "Input/Day5.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day5 Part2");

            double[] seeds = Array.ConvertAll(dataList[0].Split(':')[1].Trim().Split(' '), double.Parse);

            List<List<double[]>> input = new List<List<double[]>>();

            int counter = 3;
            while (counter < dataList.Length)
            {

                List<double[]> arrayOfLines = new List<double[]>();
                while (counter < dataList.Length)
                {
                    string line = dataList[counter];
                    if (line == "")
                    {
                        counter += 2;
                        break;
                    }
                    double[] numbers = Array.ConvertAll(dataList[counter].Split(' '), double.Parse);
                    arrayOfLines.Add(numbers);
                    counter++;
                }
                input.Add(arrayOfLines);
            }

            List<double[]> seedDomains = new List<double[]>();

            for (int i = 0; i <= seeds.Length / 2; i += 2)
            {
                seedDomains.Add(new double[]{ seeds[i], seeds[i] + seeds[i+1] - 1});
            }


            List<double[]> results = new List<double[]>();


            foreach (List<double[]> group in input)
            {
                List<double[]> cycleResults = new List<double[]>();
                while (seedDomains.Count > 0)
                {
                    double[] seedDomain = seedDomains[0];
                    seedDomains.RemoveAt(0);
                    bool added = false;

                    foreach (double[] line in group)
                    {
                        double destinationStart = line[0];
                        double sourceStart = line[1];
                        double range = line[2];

                        double domainStart = seedDomain[0];
                        double domainEnd = seedDomain[1];

                        double adjustment = destinationStart - sourceStart;

                        bool start = (domainStart >= sourceStart && domainStart <= sourceStart + range - 1);
                        bool end = (domainEnd >= sourceStart && domainEnd <= sourceStart + range - 1);

                        if (end && !start)
                        {
                            Console.WriteLine("End");
                        }

                        if (start)
                        {
                            if (end)
                            {
                                seedDomain[0] += adjustment;
                                seedDomain[1] += adjustment;
                                cycleResults.Add(seedDomain);
                                added = true;
                                break;
                            }
                            else
                            {
                                seedDomains.Add(new double[] { sourceStart + range, seedDomain[1] });
                                seedDomain[0] += adjustment;
                                seedDomain[1] = sourceStart + range - 1;
                                cycleResults.Add(seedDomain);
                                added = true;
                                break;
                            }
                        }

                        else
                        {
                            if (end)
                            {
                                seedDomains.Add(new double[] { seedDomain[0], sourceStart - 1 });
                                seedDomain[0] = destinationStart;
                                seedDomain[1] += adjustment;
                                cycleResults.Add(seedDomain);
                                added = true;
                                break;
                            }
                        }  
                    }
                    if (!added)
                    {
                        cycleResults.Add(seedDomain);
                    }
                }
                seedDomains = cycleResults;
            }

            List<double> starts = new List<double>();

            foreach (double[] domain in seedDomains)
            {
                starts.Add(domain[0]);
            }
            starts.RemoveAll(item => item == 0.0);
            Console.WriteLine(starts.Min());

        }


        public void Part1Modified()
        {
            string filePath = "Input/Day5.txt";
            var dataList = File.ReadAllLines(filePath);

            Console.WriteLine("Starting Day5 Part1");

            double[] seeds = Array.ConvertAll(dataList[0].Split(':')[1].Trim().Split(' '), double.Parse);

            List<double> seedNumbers = new List<double>();
            for (int i = 0; i<= seeds.Length/2; i += 2)
            {
                for (double j = seeds[i]; j < seeds[i] + seeds[i + 1]; j++)
                {
                    seedNumbers.Add(j);
                }
            }

            seeds = seedNumbers.ToArray();

            List<List<double[]>> input = new List<List<double[]>>();

            int counter = 3;
            while (counter < dataList.Length)
            {

                List<double[]> arrayOfLines = new List<double[]>();
                while (counter < dataList.Length)
                {
                    string line = dataList[counter];
                    if (line == "")
                    {
                        counter += 2;
                        break;
                    }
                    double[] numbers = Array.ConvertAll(dataList[counter].Split(' '), double.Parse);
                    arrayOfLines.Add(numbers);
                    counter++;
                }
                input.Add(arrayOfLines);
            }

            // Array of {x, y, z}
            // x - destination range start
            // y - source range start
            // z - range length

            List<double> results = new List<double>();

            Console.WriteLine($"{seeds.Length}");

            for (int i = 0; i < seeds.Length; i++)
            {
                double seedResult = seeds[i];
                Console.WriteLine(i);

                foreach (List<double[]> group in input)
                {
                    foreach (double[] line in group)
                    {
                        double destinationStart = line[0];
                        double sourceStart = line[1];
                        double range = line[2];

                        if (sourceStart + range > seedResult && seedResult >= sourceStart)
                        {
                            seedResult += destinationStart - sourceStart;
                            break;
                        }
                    }
                }
                results.Add(seedResult);
            }
            double minimum = results.Min();
            Console.WriteLine(minimum);
        }
    }
}
