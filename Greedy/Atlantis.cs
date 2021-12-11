using System;
using System.Linq;
using System.Collections.Generic;

public class Atlantis
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        Store[] stores = new Store[n];
        for (int i = 0; i < n; i++) // gold stores
        {
            var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            int t = arr[0]; //distance
            int h = arr[1]; //height of store
            stores[i] = new Store(t, h, i);
        }
        Array.Sort(stores, (x, y) => x.Height.CompareTo(y.Height));
        int count = 0;
        int time = 0;
        SortedSet<Store> sortedSet = new SortedSet<Store>(new Comparer()); //Add comparer
        Store worstStore;
        Store current;
        int diffDistance;
        for (int i = 0; i < n; i++)
        {
            current = stores[i];
            if (current.LatestStartingTime < time) //Can't just take 
            {
                if(count == 0) continue;
                worstStore = sortedSet.Max();
                diffDistance = worstStore.Distance - current.Distance;
                if (diffDistance > 0)
                {
                    sortedSet.Add(current);
                    sortedSet.Remove(worstStore);
                    time -= diffDistance;
                }
            }
            else
            {
                count++;
                time += current.Distance;
                sortedSet.Add(current);
            }
        }
        Console.WriteLine(count);
    }

    private class Comparer : IComparer<Store>
    {
        public int Compare(Store x, Store y)
        {
            return x.Distance.CompareTo(y.Distance);
        }
    }

    private class Store : IComparable<Store>
    {
        public int Distance { get; set; }
        public int Height { get; set; }
        public int LatestStartingTime { get; set; }
        public int Priority { get; set; }
        public Store(int t, int h, int id)
        {
            Distance = t;
            Height = h;
            LatestStartingTime = Height - Distance;
            Priority = id;
        }

        public int CompareTo(Store other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}