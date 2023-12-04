using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day2
    {

        public void Part1() 
        {
            string filePath = "Input/Day2.txt";
            using (StreamReader reader = new StreamReader(filePath))
            {

                int total_sum = 0;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //Console.WriteLine(line);

                    string[] colon_split = line.Split(':');

                    int game_id = int.Parse(colon_split[0].Trim().Split(" ")[1]);

                    //Console.WriteLine(game_id);

                    string[] games = colon_split[1].Split(";");

                    //Console.WriteLine(games);


                    bool flag = true;

                    foreach (string game in games)
                    {
                        string[] draws = game.Split(",");
                        
                        
                        foreach (string draw in draws)
                        {
                            string[] split_draw = draw.Trim().Split(" ");
                            string color = split_draw[1];
                            int quantity = int.Parse(split_draw[0]);

                            if (color == "red" && quantity > 12)
                            {
                                flag = false;
                            }

                            if (color == "green" && quantity > 13)
                            {
                                flag = false;
                            }

                            if (color == "blue" && quantity > 14)
                            {
                                flag = false;  
                            } 
                        }


                    }
                    if (flag)
                    {
                        total_sum += game_id;
                    }
                }

                Console.WriteLine(total_sum);

            }

        }


        public void Part2()
        {
            string filePath = "Input/Day2.txt";
            using (StreamReader reader = new StreamReader(filePath))
            {

                int total_sum = 0;

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] colon_split = line.Split(':');
                    int game_id = int.Parse(colon_split[0].Trim().Split(" ")[1]);
                    string[] games = colon_split[1].Split(";");

                    int red = 0;
                    int green = 0;
                    int blue = 0;

                    foreach (string game in games)
                    {
                        string[] draws = game.Split(",");


                        foreach (string draw in draws)
                        {
                            string[] split_draw = draw.Trim().Split(" ");
                            string color = split_draw[1];
                            int quantity = int.Parse(split_draw[0]);

                            if (color == "red" && quantity > red)
                            {
                                red = quantity;
                            }

                            if (color == "green" && quantity > green)
                            {
                                green = quantity;
                            }

                            if (color == "blue" && quantity > blue)
                            {
                                blue = quantity;
                            }
                        }


                    }
                        total_sum += blue * green * red;

                }

                Console.WriteLine(total_sum);

            }

        }


    }
}
