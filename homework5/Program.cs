using System;
using System.Collections.Generic;
namespace homework5;
//Создать List на 64 элемента, скачать из интернета 32 пары картинок (любых). В List
//должно содержаться по 2 одинаковых картинки.Необходимо перемешать List с
//картинками. Вывести в консоль перемешанные номера (изначальный List и полученный
//List). Перемешать любым способом.
class Program
{
    static void Main()
    {
        List<string> images = new List<string>();

        for (int i = 1; i <= 32; i++)
        {
            string image = "image" + i;
            images.Add(image);
            images.Add(image);
        }

        Console.WriteLine("Исходный список:");
        PrintList(images);

        Shuffle(images);

        Console.WriteLine("\nПеремешанный список:");
        PrintList(images);
    }

    static void Shuffle<T>(List<T> list)
    {
        Random rand = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    static void PrintList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"Индекс {i + 1}: {list[i]}");
        }
    }
}
