using System.Collections.Generic;
using System;
using System.Linq;
public static class ProfitablePizzas
{
    public static void Main(string[] args)
    {
        Run();
    }

    public static void Run()
    {
        var n = int.Parse(Console.ReadLine());
        var list = new List<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            list.Add((arr[0], arr[1]));
        }
        int[] slots = new int[n];
        var queue = new PriorityQueue<(int, int), int>(); //x = value of pizza, y = index in slots
        int i1 = 0; // skal nok starte fra 1
        foreach (var x in list.OrderBy(x => x.Item1))
        {
            if (x.Item1-1 < i1)
            {
                if (queue.Peek().Item2 < x.Item2)
                {
                    var removed = queue.Dequeue();
                    slots[removed.Item1] = x.Item2;
                    queue.Enqueue((removed.Item1, x.Item2), x.Item2);
                }
            }
            else
            {
                slots[i1] = x.Item2;
                queue.Enqueue((i1, x.Item2), x.Item2);
                i1++;
            }
        }

        int sum = 0;
        foreach (var x in queue.UnorderedItems) sum += x.Priority;
        Console.WriteLine(sum);
    }
    
    public static void RunHighestPrice()
    {
        var n = int.Parse(Console.ReadLine());
        var list = new List<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            list.Add((arr[0], arr[1]));
        }

        int[] slots = new int[2*10^6+1];
        foreach (var x in list.OrderByDescending(x => x.Item2))
        {
            int i = x.Item1;
            while (i > -1)
            {
                if (slots[i] == 0)
                {
                    slots[i] = x.Item2;
                    break;
                }
                i--;
            }
        }
        long sum = 0;
        foreach (var x in slots) sum += x;
        Console.WriteLine(sum);
    }
}