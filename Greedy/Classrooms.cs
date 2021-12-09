using System.Linq;
using System;
using System.Collections.Generic;

public class Classrooms
{
    public static void Main()
    {
        var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        int n = arr[0];
        int k = arr[1];

        string[] a;
        int s;
        int f;
        Activity[] activities = new Activity[n];
        for (int i = 0; i < n; i++)
        {
            a = Console.ReadLine().Split(' ');
            s = int.Parse(a[0]);
            f = int.Parse(a[1]);
            activities[i] = new Activity(s, f, i);
        }
        if(n == k) {Console.WriteLine(n); return;}
        Array.Sort(activities, (x, y) => x.Finish.CompareTo(y.Finish));

        SortedSet<Activity> rooms = new SortedSet<Activity>(new Comparer());
        int count = 0;
        foreach(Activity current in activities)
        {
            //if (rooms.Last != null && rooms.Last.Value.Finish < current.Start)
            if(rooms.Count > 0 && rooms.Min.Finish < current.Start)
            {
                var set = rooms.GetViewBetween(new Activity(0, 0, 0), new Activity(0, current.Start, 0));
                rooms.Remove(set.Max);
                rooms.Add(current);
                count++;
            }
            else if (rooms.Count < k)
            {
                rooms.Add(current);
                count++;
            }
        }
        Console.WriteLine(count);
    }
    private class Activity
    {
        public int Count { get; set; }
        public int Start { get; }
        public int Finish { get; }
        public Activity(int s, int f, int count)
        {
            Count = count;
            Start = s;
            Finish = f;
        }
    }

    private class Comparer : IComparer<Activity>
    {
        public int Compare(Activity a, Activity b)
        {
            return (a.Finish != b.Finish) ? a.Finish.CompareTo(b.Finish) : a.Count.CompareTo(b.Count);
        }
    }
}