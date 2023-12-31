using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day13
    {     
        public void Part1()
        {
            string filePath = "Input/Day13.txt";
            var dataList = File.ReadAllLines(filePath).ToList();

            int result = 0;

            List<List<string>> input = new List<List<string>>();
            while (dataList.Count > 0)
            {
                List<string> map = new List<string>();

                while (dataList.Count > 0)
                {
                    string line = dataList[0];
                    dataList.RemoveAt(0);

                    if (line != "")
                    {
                        map.Add(line);
                    }

                    else
                    {
                        break;
                    }
                }
                input.Add(map);
            }
         
            List<List<int>> rows = new List<List<int>>();
            List<List<int>> columns = new List<List<int>>();  

            foreach (List<string> map in input)
            {
                // Rows
                List<int> rowNumbers = new List<int>();
                foreach (string row in map)
                {
                    
                    StringBuilder stringBuilder= new StringBuilder();
                    foreach(char c in row)
                    {
                        if (c == '#')
                        {
                            stringBuilder.Append("1");
                        }
                        else
                        {
                            stringBuilder.Append("0");
                        }
                    }
                    int rowNumber = Convert.ToInt32(stringBuilder.ToString(), 2);
                    rowNumbers.Add(rowNumber);
                }
                rows.Add(rowNumbers);

                bool rowCheck = false;

                for (int i = 0; i < rowNumbers.Count - 1; i++)
                {
                    int maxRange = Math.Min(i, rowNumbers.Count - 2 - i);
                    rowCheck = true;
                    
                    for (int j = 0; j <= maxRange; j++)
                    {
                        int indexFirst = i - j;
                        int indexSecond = i + 1 + j;
                        int number1 = rowNumbers[indexFirst];
                        int number2 = rowNumbers[indexSecond];

                        if (number1 != number2)
                        {
                            rowCheck = false;
                            break;
                        }
                    }
                    if (rowCheck)
                    {
                        result += 100 * (i + 1);
                        break;
                    }
                }

                if (!rowCheck)
                {
                    List<int> columnNumbers = new List<int>();
                    for (int i = 0; i < map[0].Length; i++)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (string row in map)
                        {
                            char c = row[i];
                            if (c == '#')
                            {
                                stringBuilder.Append("1");
                            }
                            else
                            {
                                stringBuilder.Append("0");
                            }
                        }
                        int columnNumber = Convert.ToInt32(stringBuilder.ToString(), 2);
                        columnNumbers.Add(columnNumber);
                    }
                    columns.Add(columnNumbers);



                    bool columnCheck = false;

                    for (int i = 0; i < columnNumbers.Count - 1; i++)
                    {
                        int maxRange = Math.Min(i, columnNumbers.Count - 2 - i);
                        columnCheck = true;

                        for (int j = 0; j <= maxRange; j++)
                        {
                            int indexFirst = i - j;
                            int indexSecond = i + 1 + j;
                            int number1 = columnNumbers[indexFirst];
                            int number2 = columnNumbers[indexSecond];

                            if (number1 != number2)
                            {
                                columnCheck = false;
                                break;
                            }
                        }
                        if (columnCheck)
                        {
                            result += i + 1;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(result);
        }


        public void Part2()
        {
            string filePath = "Input/Day13.txt";
            var dataList = File.ReadAllLines(filePath).ToList();

            int result = 0;

            List<List<string>> input = new List<List<string>>();
            while (dataList.Count > 0)
            {
                List<string> map = new List<string>();

                while (dataList.Count > 0)
                {
                    string line = dataList[0];
                    dataList.RemoveAt(0);

                    if (line != "")
                    {
                        map.Add(line);
                    }

                    else
                    {
                        break;
                    }
                }
                input.Add(map);
            }





            foreach (List<string> map in input)
            {

                int ogResult = 0;

                List<int> ogRowNumbers = new List<int>();
                foreach (string row in map)
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (char c in row)
                    {
                        if (c == '#')
                        {
                            stringBuilder.Append("1");
                        }
                        else
                        {
                            stringBuilder.Append("0");
                        }
                    }
                    int rowNumber = Convert.ToInt32(stringBuilder.ToString(), 2);
                    ogRowNumbers.Add(rowNumber);
                }

                bool ogRowCheck = false;

                for (int i = 0; i < ogRowNumbers.Count - 1; i++)
                {
                    int maxRange = Math.Min(i, ogRowNumbers.Count - 2 - i);
                    ogRowCheck = true;

                    for (int j = 0; j <= maxRange; j++)
                    {
                        int indexFirst = i - j;
                        int indexSecond = i + 1 + j;
                        int number1 = ogRowNumbers[indexFirst];
                        int number2 = ogRowNumbers[indexSecond];

                        if (number1 != number2)
                        {
                            ogRowCheck = false;
                            break;
                        }
                    }
                    if (ogRowCheck)
                    {
                        ogResult = 100 * (i + 1);
                        break;
                    }
                }

                if (!ogRowCheck)
                {
                    List<int> ogColumnNumbers = new List<int>();
                    for (int i = 0; i < map[0].Length; i++)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        foreach (string row in map)
                        {
                            char c = row[i];
                            if (c == '#')
                            {
                                stringBuilder.Append("1");
                            }
                            else
                            {
                                stringBuilder.Append("0");
                            }
                        }
                        int columnNumber = Convert.ToInt32(stringBuilder.ToString(), 2);
                        ogColumnNumbers.Add(columnNumber);
                    }

                    bool ogColumnCheck = false;

                    for (int i = 0; i < ogColumnNumbers.Count - 1; i++)
                    {
                        int maxRange = Math.Min(i, ogColumnNumbers.Count - 2 - i);
                        ogColumnCheck = true;

                        for (int j = 0; j <= maxRange; j++)
                        {
                            int indexFirst = i - j;
                            int indexSecond = i + 1 + j;
                            int number1 = ogColumnNumbers[indexFirst];
                            int number2 = ogColumnNumbers[indexSecond];

                            if (number1 != number2)
                            {
                                ogColumnCheck = false;
                                break;
                            }
                        }
                        if (ogColumnCheck)
                        {
                            ogResult += i + 1;
                            break;
                        }
                    }
                }


                bool resultAdded = false;

                for (int rowLineNumber = 0; rowLineNumber < map.Count; rowLineNumber++)
                {

                    for (int columnLineNumber = 0; columnLineNumber < map[0].Length; columnLineNumber++)
                    {
                        char[] charArray = map[rowLineNumber].ToCharArray();
                        char smudge = charArray[columnLineNumber];

                        if (smudge == '#')
                        {
                            charArray[columnLineNumber] = '.';
                        }
                        else
                        {
                            charArray[columnLineNumber] = '#';
                        }

                        string modifiedLine = new string(charArray);

                        List<string> mapCopy = new List<string>(map);
                        mapCopy[rowLineNumber] = modifiedLine;

                        // Rows
                        List<int> rowNumbers = new List<int>();
                        foreach (string row in mapCopy)
                        {

                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (char c in row)
                            {
                                if (c == '#')
                                {
                                    stringBuilder.Append("1");
                                }
                                else
                                {
                                    stringBuilder.Append("0");
                                }
                            }
                            int rowNumber = Convert.ToInt32(stringBuilder.ToString(), 2);
                            rowNumbers.Add(rowNumber);
                        }

                        bool rowCheck = false;

                        for (int i = 0; i < rowNumbers.Count - 1; i++)
                        {
                            int maxRange = Math.Min(i, rowNumbers.Count - 2 - i);
                            rowCheck = true;

                            for (int j = 0; j <= maxRange; j++)
                            {
                                int indexFirst = i - j;
                                int indexSecond = i + 1 + j;
                                int number1 = rowNumbers[indexFirst];
                                int number2 = rowNumbers[indexSecond];

                                if (number1 != number2)
                                {
                                    rowCheck = false;
                                    break;
                                }
                            }
                            if (rowCheck)
                            {
                                int thisResult = 100 * (i + 1);

                                if (thisResult != ogResult)
                                {
                                    resultAdded = true;
                                    result += 100 * (i + 1);
                                    break;
                                }
                                else
                                {
                                    rowCheck = false;
                                }
                            }
                        }

                        if (!rowCheck)
                        {
                            List<int> columnNumbers = new List<int>();
                            for (int i = 0; i < mapCopy[0].Length; i++)
                            {
                                StringBuilder stringBuilder = new StringBuilder();
                                foreach (string row in mapCopy)
                                {
                                    char c = row[i];
                                    if (c == '#')
                                    {
                                        stringBuilder.Append("1");
                                    }
                                    else
                                    {
                                        stringBuilder.Append("0");
                                    }
                                }
                                int columnNumber = Convert.ToInt32(stringBuilder.ToString(), 2);
                                columnNumbers.Add(columnNumber);
                            }

                            bool columnCheck = false;

                            for (int i = 0; i < columnNumbers.Count - 1; i++)
                            {
                                int maxRange = Math.Min(i, columnNumbers.Count - 2 - i);
                                columnCheck = true;

                                for (int j = 0; j <= maxRange; j++)
                                {
                                    int indexFirst = i - j;
                                    int indexSecond = i + 1 + j;
                                    int number1 = columnNumbers[indexFirst];
                                    int number2 = columnNumbers[indexSecond];

                                    if (number1 != number2)
                                    {
                                        columnCheck = false;
                                        break;
                                    }
                                }
                                if (columnCheck)
                                {
                                    int thisResult = i + 1;

                                    if (thisResult != ogResult)
                                    {
                                        resultAdded = true;
                                        result += i + 1;
                                        break;
                                    }


                                }
                            }
                        }

                        if (resultAdded)
                        {
                            break;
                        }
                    }


                    if (resultAdded)
                    {
                        break;
                    }
                }

            }

            Console.WriteLine(result);


        }            
    }
}
