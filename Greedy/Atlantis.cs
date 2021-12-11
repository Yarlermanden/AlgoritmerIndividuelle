using System;
using System.Linq;

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
            stores[i] = new Store(t, h);
        }
        //Array.Sort(stores, (x,y) => (x.LatestStartingTime != y.LatestStartingTime) ? x.LatestStartingTime.CompareTo(y.LatestStartingTime) : x.Distance.CompareTo(y.Distance));
        Array.Sort(stores, (x, y) => (x.LatestStartingTime != y.LatestStartingTime) ? y.LatestStartingTime.CompareTo(x.LatestStartingTime) : x.Distance.CompareTo(y.Distance));
        //Array.Sort(stores, (x,y) => (x.LatestPoint != y.LatestPoint) ? x.LatestPoint.CompareTo(y.LatestPoint) : y.Distance.CompareTo(x.Distance));
        int count = 0;
        //long time = stores[0].Height;
        long time = stores[0].Height;
        for (int i = 0; i < n; i++)
        {
            //if (time + stores[i].Distance < stores[i].Height)
            //if(time >= stores[i].Height)
            //if(time >= stores[i].LatestStartingTime)
            if(time-stores[i].Distance >= 0)
                //seneste starting tidspunkt + distance - hvis den overstiger time,
            {
                count++;
                time -= stores[i].Distance;
                Console.WriteLine("taking " + stores[i].LatestStartingTime + " which sets time to " + time);
            }
            else
            {
                Console.WriteLine("Didn't take: " + stores[i].LatestStartingTime + " as " + time + " + " + stores[i].Distance + " is larger than " + stores[i].Height);
            }
        }
        Console.WriteLine(count);
    }

    private class Store
    {
        public int Distance { get; set; }
        public int Height { get; set; }
        public long LatestStartingTime { get; set; }
        public Store(int t, int h)
        {
            Distance = t;
            Height = h;
            //LatestPoint = Distance + Height;
            LatestStartingTime = Height - Distance;
        }
    }
}