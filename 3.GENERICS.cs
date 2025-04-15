using System;
using System.Linq;

public class SimpleSearch
{
    public static int BinarySearch<T>(T[] arr, T key) where T : IComparable<T>
    {
        int left = 0, right = arr.Length - 1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;
            int comparison = arr[middle].CompareTo(key);

            if (comparison == 0)
                return middle;
            if (comparison < 0)
                left = middle + 1;
            else
                right = middle - 1;
        }

        return -1;
    }

    public static int GetValidIntInput(string prompt)
    {
        int result;
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out result))
                return result;
            else
                Console.WriteLine("Invalid input! Please enter a valid integer.");
        }
    }

    public static string GetValidStringInput(string prompt)
    {
        string input;
        while (true)
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && input.All(char.IsLetter))
                return input;
            else
                Console.WriteLine("Invalid input! Please enter a valid string with alphabetic characters only.");
        }
    }

    public static void Main()
    {
        // Integer Array Section
        Console.WriteLine("Enter the number of integers in the array:");
        int n = int.Parse(Console.ReadLine());

        int[] numArray = new int[n];
        for (int i = 0; i < n; i++)
            numArray[i] = GetValidIntInput($"Enter integer {i + 1}: ");

        Array.Sort(numArray);
        Console.WriteLine("Sorted number array: " + string.Join(", ", numArray));

        int numKey = GetValidIntInput("Enter the number to search for in the number array: ");
        int numIndex = BinarySearch(numArray, numKey);

        if (numIndex != -1)
            Console.WriteLine($"Number {numKey} found at index: {numIndex}");
        else
            Console.WriteLine($"Number {numKey} not found in the number array.");

        // String Array Section
        Console.WriteLine("Enter the number of strings in the array:");
        int m = int.Parse(Console.ReadLine());

        string[] strArray = new string[m];
        for (int i = 0; i < m; i++)
            strArray[i] = GetValidStringInput($"Enter string {i + 1}: ");

        Array.Sort(strArray);
        Console.WriteLine("Sorted string array: " + string.Join(", ", strArray));

        string strKey = GetValidStringInput("Enter the string to search for in the string array: ");
        int strIndex = BinarySearch(strArray, strKey);

        if (strIndex != -1)
            Console.WriteLine($"String \"{strKey}\" found at index: {strIndex}");
        else
            Console.WriteLine($"String \"{strKey}\" not found in the string array.");
    }
}
