using AdventOfCode2023;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

class Program
{

    static void ChangeDot(List<List<char>> input)
    {
        foreach( List<char> line in input )
        {
            line[0] = '#';
        }
    }

    static void Main()
    {
       Day17 day = new Day17();
        day.Part1();
        //day.Part2();

        foreach (var l in input)
            Console.WriteLine(string.Join(",", l));
    }
}