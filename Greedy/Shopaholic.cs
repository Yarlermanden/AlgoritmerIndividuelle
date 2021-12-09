using System;
using System.Linq;

public class Shopaholic
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        Array.Sort(arr, (x, y) => y.CompareTo(x));
        int i = 2;
        long sum = 0;
        while (i < n)
        {
            sum += arr[i];
            i += 3;
        }
        Console.WriteLine(sum);
    }
}