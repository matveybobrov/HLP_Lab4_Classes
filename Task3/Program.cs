using System;
using System.Linq;
using System.Collections.Generic;

class Arr
{
    int[] items;

    public Arr(int size)
    {
        this.items = new int[size];
    }

    public void InputData()
    {
        Console.WriteLine("Заполните массив (новый элемент на новой строке)");
        for (int i = 0; i < items.Length; i++)
        {
            int item;
            try
            {
                item = Convert.ToInt16(Console.ReadLine());
            } catch (FormatException)
            {
                Console.WriteLine("Введено не число, ставим 0");
                item = 0;
            }
            this.items[i] = item;
        }
    }

    public void InputDataRandom()
    {
        Console.WriteLine("Заполняем массив случайными числами...");
        Random rnd = new Random();
        for (int i = 0; i < items.Length; i++)
        {
            int item = rnd.Next(0, 256);
            this.items[i] = item;
        }
    }

    public void Print(int start = -1, int end = -1)
    {
        if (start < 0 || start > this.items.Length)
        {
            Console.WriteLine("Начало установлено в 0");
            start = 0;
        }
        if (end < start || end > this.items.Length)
        {
            Console.WriteLine($"Конец установлен в {this.items.Length}");
            end = this.items.Length;
        }
        for (int i = start; i < end; i++)
        {
            Console.Write($"{items[i]} ");
        }
        Console.Write("\n");
    }

    public int FindMax()
    {
        return this.items.Max();
    }

    public Arr Add(Arr arr2)
    {
        if (arr2.GetLength() != this.GetLength())
        {
            Console.WriteLine("Размеры массивов не совпадают");
            return this;
        }
        for (int i = 0; i < this.items.Length; i++)
        {
            this.items[i] = this.items[i] + arr2.items[i];
        }
        return this;
    }

    public Arr Sort(ref Arr arr)
    {
        for (int i = 0; i < arr.GetLength(); i++)
        {
            for (int j = 0; j < arr.GetLength() - 1 - i; j++)
            {
                if (arr.items[j] > arr.items[j + 1])
                {
                    int temp = arr.items[j];
                    arr.items[j] = arr.items[j + 1];
                    arr.items[j + 1] = temp;
                }
            }
        }
        return this;
    }

    public int[] FindValue(in int item)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < this.items.Length; i++)
        {
            if (this.items[i] == item)
            {
                indexes.Add(i);
            }
        }
        int[] result = new int[indexes.Count];
        for (int i = 0; i < indexes.Count; i++)
        {
            result[i] = indexes[i];
        }
        return result;
    }

    public Arr DelValue(int item)
    {
        int[] indexes = this.FindValue(item);
        if (indexes.Length == 0)
        {
            Console.WriteLine("Такого элемента нет в массиве");
            return this;
        }
        Arr newArr = new Arr(this.items.Length - indexes.Length);
        int j = 0;
        for (int i = 0; i < this.items.Length; i++)
        {
            if (this.items[i] != item)
            {
                newArr.items[j] = this.items[i];
                j++;
            }
        }
        this.items = newArr.items;
        return this;
    }

    public int GetLength()
    {
        return this.items.Length;
    }
}

public class Program
{
    public static int GetArraySize(out int size)
    {
        Console.WriteLine("Введите размер массива");
        try
        {
            size = Convert.ToInt16(Console.ReadLine());
        }
        catch (FormatException)
        {
            Console.WriteLine("Некорректный размер!");
            return GetArraySize(out size);
        }
        if (size <= 0)
        {
            Console.WriteLine("Размер массива должен быть положительным числом");
            return GetArraySize(out size);
        }
        return size;
    }
    public static void Main()
    {
        int size = 0;
        GetArraySize(out size);
        Arr arr = new Arr(size);
        Console.WriteLine("Вводите операции...\n");
        Console.WriteLine("Print start end - вывести массив");
        Console.WriteLine("InputData       - заполнить массив вручную");
        Console.WriteLine("InputDataRandom - заполнить массив случайными числами");
        Console.WriteLine("Sort            - отсортировать массив");
        Console.WriteLine("FindMax         - найти максимальное значение массива");
        Console.WriteLine("FindValue value - найти все индексы искомого значения");
        Console.WriteLine("DelValue value  - удалить значения из массива");
        Console.WriteLine("Exit            - выход\n");
        while (true)
        {
            string prompt = Console.ReadLine();
            string[] substrings = prompt.Split(' ');
            switch (substrings[0])
            {
                case "Print":
                    int start;
                    int end;
                    try
                    {
                        start = Convert.ToInt16(substrings[1]);
                    } catch(IndexOutOfRangeException)
                    {
                        start = 0;
                    }
                    try
                    {
                        end = Convert.ToInt16(substrings[2]);
                    } catch(IndexOutOfRangeException)
                    {
                        end = arr.GetLength();
                    }
                    arr.Print(start, end);
                    break;
                case "InputData":
                    arr.InputData();
                    Console.WriteLine("Данные успешно записаны");
                    break;
                case "InputDataRandom":
                    arr.InputDataRandom();
                    Console.WriteLine("Данные успешно записаны");
                    break;
                case "FindMax":
                    Console.WriteLine($"Максимальное значение: {arr.FindMax()}");
                    break;
                case "Sort":
                    arr.Sort(ref arr);
                    Console.WriteLine("Массив успешно отсортирован");
                    break;
                case "FindValue":
                    int value1 = Convert.ToInt16(substrings[1]);
                    int[] indexes = arr.FindValue(value1);
                    if (indexes.Length == 0)
                    {
                        Console.WriteLine("Такого элемента нет в массиве");
                        break;
                    }
                    Console.WriteLine("Индексы искомого элемента:");
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        Console.Write($"{indexes[i]} ");
                    }
                    break;
                case "DelValue":
                    try
                    {
                        int value2 = Convert.ToInt16(substrings[1]);
                        arr.DelValue(value2);
                        Console.WriteLine("Операция выполнена");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Не удалось удалить элемент");
                    }
                    break;
                case "Exit":
                    Console.WriteLine("bye");
                    return;
                default:
                    Console.WriteLine("Такой операции нет");
                    break;
            }
            Console.WriteLine();
        }
        /*
        Arr arr1 = new Arr(5);
        arr1.InputDataRandom();
        arr1.Print();
        Arr arr2 = new Arr(6);
        arr2.InputDataRandom();
        arr2.Print();
        arr1.Add(arr2).Print();
        */
    }
}