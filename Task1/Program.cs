using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    public static int GetStringsCount()
    {
        Console.WriteLine("Введите количество строк:");
        try
        {
            int stringsCount = Convert.ToInt16(Console.ReadLine());
            if (stringsCount <= 0)
            {
                Console.WriteLine("Число должно быть положительным");
                return GetStringsCount();
            }
            return stringsCount;
        }
        catch (FormatException)
        {
            Console.WriteLine("Вы ввели не целое число!");
            return GetStringsCount();
        }
    }

    public static int[] ParseString(in string str)
    {
        List<int> result = new List<int>();
        string buff = "";
        for (int i = 0; i < str.Length; i++)
        {
            string curr = Convert.ToString(str[i]);
            var isNumeric = int.TryParse(curr, out int n);
            if (isNumeric)
            {
                buff += curr;
            }
            else if (curr == " ")
            {
                if (buff != "")
                {
                    result.Add(Convert.ToInt32(buff));
                }
                buff = "";
            }
            else
            {
                buff += "0";
            }
        }
        if (buff != "") result.Add(Convert.ToInt32(buff));
        return result.ToArray();
    }

    public static int[] GetMinMaxSum(ref int[] numbers)
    {
        int max = numbers.Max();
        int min = numbers.Min();
        int sum = numbers.Sum();
        return new int[] { min, max, sum };
    }

    public static void Main()
    {
        int stringsCount = GetStringsCount();

        Console.WriteLine($"\nВведите {stringsCount} строк:");
        int[][] strings = new int[stringsCount][];
        for (int i = 0; i < stringsCount; i++)
        {
            string newString = Console.ReadLine();
            int[] arr = ParseString(newString);
            strings[i] = arr;
        }

        Console.WriteLine("\nРезультат преобразования:");
        for (int i = 0; i < strings.Length; i++)
        {
            for (int j = 0; j < strings[i].Length; j++)
            {
                Console.Write(strings[i][j] + " ");
            }
            Console.Write("\n");
        }

        Console.WriteLine("\nРезультат операций:");
        for (int i = 0; i < strings.Length; i++)
        {
            int[] results = GetMinMaxSum(ref strings[i]);
            Console.WriteLine($"{i} string: min:{results[0]}, max:{results[1]}, sum:{results[2]}");
        }
    }
}