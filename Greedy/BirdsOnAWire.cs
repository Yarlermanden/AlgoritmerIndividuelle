using System;
using System.Linq;

public class BirdsOnAWire
{
    public static void Main()
    {
        var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        int l = arr[0]; //wire length
        int d = arr[1]; //min distance between each bird
        int n = arr[2]; //number of birds at start
        int[] birds = new int[n];
        for (int i = 0; i < n; i++)
        {
            birds[i] = int.Parse(Console.ReadLine());
        }
        Array.Sort(birds, (x,y) => x.CompareTo(y));
        int count = 0;
        int position = 6;
        int j = 0;
        int endPosition = l - 5;
        while (position < endPosition)
        {
            if (j == birds.Length || position + d-1 < birds[j]) //add a bird
            {
                count++;
                position += d;
            }
            else //not enough space - check after bird
            {
                position = birds[j]+d;
                j++;
            }
        }
        Console.WriteLine(count);
    }
}