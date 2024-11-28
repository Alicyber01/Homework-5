using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tumakovlabatory_6;
class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите задачу:");
        Console.WriteLine("1 - Подсчет гласных и согласных в файле");
        Console.WriteLine("2 - Умножение матриц");
        Console.WriteLine("3 - Средняя температура по месяцам");
        Console.WriteLine("4 - Подсчет гласных и согласных в файле с использованием List<char>");
        Console.WriteLine("5 - Умножение матриц с использованием LinkedList<LinkedList<int>>");
        Console.WriteLine("6 - Средняя температура по месяцам с использованием Dictionary<string, int[]>");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                ProcessFile();
                break;
            case "2":
                ProcessMatrices();
                break;
            case "3":
                ProcessTemperature();
                break;
            case "4":
                ProcessFileWithList();
                break;
            case "5":
                ProcessMatricesWithLinkedList();
                break;
            case "6":
                ProcessTemperatureWithDictionary();
                break;
            default:
                Console.WriteLine("Некорректный выбор.");
                break;
        }
    }

       // Exercise 1: Написать программу, которая вычисляет число гласных и согласных букв в
      //файле.Имя файла передавать как аргумент в функцию Main.Содержимое текстового файла
     //заносится в массив символов. Количество гласных и согласных букв определяется проходом
    //по массиву.Предусмотреть метод, входным параметром которого является массив символов.
   //Метод вычисляет количество гласных и согласных букв.
    static void ProcessFile()
    {
        Console.WriteLine("Введите имя файла:");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Файл \"{fileName}\" не найден.");
            return;
        }

        try
        {
            string content = File.ReadAllText(fileName);
            (int vowels, int consonants) = CountVowelsAndConsonants(content);
            Console.WriteLine($"Гласных: {vowels}, Согласных: {consonants}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static (int vowels, int consonants) CountVowelsAndConsonants(string content)
    {
        int vowels = 0, consonants = 0;
        string vowelsSet = "aeiouyаеёиоуыэюя";

        foreach (char c in content)
        {
            if (char.IsLetter(c))
            {
                char lowerC = char.ToLower(c);
                if (vowelsSet.Contains(lowerC))
                    vowels++;
                else
                    consonants++;
            }
        }
        return (vowels, consonants);
    }

    // Exercise 2: Написать программу, реализующую умножению двух матриц, заданных в
   //виде двумерного массива.В программе предусмотреть два метода: метод печати матрицы,
  //метод умножения матриц(на вход две матрицы, возвращаемое значение – матрица).
    static void ProcessMatrices()
    {
        int[,] matrixA = { { 1, 2, 3 }, { 4, 5, 6 } };
        int[,] matrixB = { { 7, 8 }, { 9, 10 }, { 11, 12 } };

        Console.WriteLine("Матрица A:");
        PrintMatrix(matrixA);

        Console.WriteLine("Матрица B:");
        PrintMatrix(matrixB);

        try
        {
            int[,] result = MultiplyMatrices(matrixA, matrixB);
            Console.WriteLine("Результат умножения матриц:");
            PrintMatrix(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        int rowsA = matrixA.GetLength(0);
        int colsA = matrixA.GetLength(1);
        int rowsB = matrixB.GetLength(0);
        int colsB = matrixB.GetLength(1);

        if (colsA != rowsB)
        {
            throw new InvalidOperationException("Количество столбцов матрицы A должно совпадать с количеством строк матрицы B.");
        }

        int[,] result = new int[rowsA, colsB];
        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }
        return result;
    }

    static void PrintMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }

        // Exercise 3: Написать программу, вычисляющую среднюю температуру за год. Создать
       // двумерный рандомный массив temperature[12, 30], в котором будет храниться температура
      //для каждого дня месяца(предполагается, что в каждом месяце 30 дней). Сгенерировать
     //значения температур случайным образом.Для каждого месяца распечатать среднюю
    //температуру.Для этого написать метод, который по массиву temperature [12, 30] для каждого
   //месяца вычисляет среднюю температуру в нем, и в качестве результата возвращает массив
  //средних температур. Полученный массив средних температур отсортировать по
 //возрастанию.

    static void ProcessTemperature()
    {
        Random rand = new Random();
        int[,] temperature = new int[12, 30];
        FillTemperatureArray(temperature, rand);
        double[] averageTemperatures = CalculateMonthlyAverages(temperature);

        Console.WriteLine("Средняя температура по месяцам:");
        for (int i = 0; i < averageTemperatures.Length; i++)
        {
            Console.WriteLine($"Месяц {i + 1}: {averageTemperatures[i]:F2}°C");
        }

        Array.Sort(averageTemperatures);
        Console.WriteLine("\nСредние температуры по месяцам после сортировки:");
        foreach (var temp in averageTemperatures)
        {
            Console.WriteLine($"{temp:F2}°C");
        }
    }

    static void FillTemperatureArray(int[,] temperature, Random rand)
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                temperature[i, j] = rand.Next(-30, 41);
            }
        }
    }

    static double[] CalculateMonthlyAverages(int[,] temperature)
    {
        double[] averages = new double[12];
        for (int i = 0; i < 12; i++)
        {
            double sum = 0;
            for (int j = 0; j < 30; j++)
            {
                sum += temperature[i, j];
            }
            averages[i] = sum / 30.0;
        }
        return averages;
    }

    // Exercise 4:Exercise 1. Выполнить с помощью коллекции List<T>.
    static void ProcessFileWithList()
    {
        Console.WriteLine("Введите имя файла:");
        string fileName = Console.ReadLine();

        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Файл \"{fileName}\" не найден.");
            return;
        }

        try
        {
            string content = File.ReadAllText(fileName);
            List<char> charList = new List<char>(content);
            (int vowels, int consonants) = CountVowelsAndConsonantsWithList(charList);
            Console.WriteLine($"Гласных: {vowels}, Согласных: {consonants}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static (int vowels, int consonants) CountVowelsAndConsonantsWithList(List<char> charList)
    {
        int vowels = 0, consonants = 0;
        string vowelsSet = "aeiouyаеёиоуыэюя";

        foreach (char c in charList)
        {
            if (char.IsLetter(c))
            {
                char lowerC = char.ToLower(c);
                if (vowelsSet.Contains(lowerC))
                    vowels++;
                else
                    consonants++;
            }
        }
        return (vowels, consonants);
    }

    // Exercise 5:Exercise 2. Выполнить с помощью коллекций LinkedList<LinkedList<T>>.
    static void ProcessMatricesWithLinkedList()
    {
        LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>();
        LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>();

        InitializeMatrix(matrixA, new int[,] { { 1, 2, 3 }, { 4, 5, 6 } });
        InitializeMatrix(matrixB, new int[,] { { 7, 8 }, { 9, 10 }, { 11, 12 } });

        Console.WriteLine("Матрица A:");
        PrintMatrix(matrixA);

        Console.WriteLine("Матрица B:");
        PrintMatrix(matrixB);

        try
        {
            LinkedList<LinkedList<int>> result = MultiplyMatricesWithLinkedList(matrixA, matrixB);
            Console.WriteLine("Результат умножения матриц:");
            PrintMatrix(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void InitializeMatrix(LinkedList<LinkedList<int>> matrix, int[,] array)
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            LinkedList<int> row = new LinkedList<int>();
            for (int j = 0; j < array.GetLength(1); j++)
            {
                row.AddLast(array[i, j]);
            }
            matrix.AddLast(row);
        }
    }

    static LinkedList<LinkedList<int>> MultiplyMatricesWithLinkedList(LinkedList<LinkedList<int>> matrixA, LinkedList<LinkedList<int>> matrixB)
    {
        var result = new LinkedList<LinkedList<int>>();
        var rowA = matrixA.First;
        var colB = matrixB.First;

        while (rowA != null)
        {
            var resultRow = new LinkedList<int>();
            var colNode = matrixB.First;
            while (colNode != null)
            {
                int sum = 0;
                var elementA = rowA.Value.GetEnumerator();
                var elementB = colNode.Value.GetEnumerator();

                while (elementA.MoveNext() && elementB.MoveNext())
                {
                    sum += elementA.Current * elementB.Current;
                }
                resultRow.AddLast(sum);
                colNode = colNode.Next;
            }
            result.AddLast(resultRow);
            rowA = rowA.Next;
        }

        return result;
    }

    static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
    {
        foreach (var row in matrix)
        {
            foreach (var value in row)
            {
                Console.Write(value + "\t");
            }
            Console.WriteLine();
        }
    }

      // Exercise 6:Написать программу для Exercise 3,использовав класс
     //Dictionary<TKey, TValue>.В качестве ключей выбрать строки – названия месяцев, а в
    //качестве значений – массив значений температур по дням.
    static void ProcessTemperatureWithDictionary()
    {
        Dictionary<string, int[]> monthlyTemps = new Dictionary<string, int[]>
        {
            { "January", new int[30] },
            { "February", new int[30] },
            { "March", new int[30] },
            { "April", new int[30] },
            { "May", new int[30] },
            { "June", new int[30] },
            { "July", new int[30] },
            { "August", new int[30] },
            { "September", new int[30] },
            { "October", new int[30] },
            { "November", new int[30] },
            { "December", new int[30] }
        };

        Random rand = new Random();
        foreach (var month in monthlyTemps.Keys)
        {
            for (int i = 0; i < 30; i++)
            {
                monthlyTemps[month][i] = rand.Next(-30, 41);
            }
        }

        Dictionary<string, double> monthlyAverages = new Dictionary<string, double>();
        foreach (var month in monthlyTemps)
        {
            double average = month.Value.Average();
            monthlyAverages.Add(month.Key, average);
        }

        Console.WriteLine("Средняя температура по месяцам:");
        foreach (var month in monthlyAverages)
        {
            Console.WriteLine($"{month.Key}: {month.Value:F2}°C");
        }
    }
}
