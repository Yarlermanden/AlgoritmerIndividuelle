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
            activities[i] = new Activity(s, f);
        }
        if(n == k) {Console.WriteLine(n); return;}
        Array.Sort(activities, (x, y) => x.Finish.CompareTo(y.Finish));

        LinkedList rooms = new LinkedList();
        int count = 0;
        foreach(Activity current in activities)
        {
            if (rooms.Last != null && rooms.Last.Value.Finish < current.Start)
            {
                LinkedListNode previousNode = null;
                var node = rooms.First;
                while (node != null)
                {
                    var next = node.Next;
                    if (node.Value.Finish < current.Start)
                    {
                        count++;
                        if (previousNode == null)
                        {
                            if (rooms.First == rooms.Last) rooms.Last = next;
                            rooms.First = next;
                        }
                        else
                        {
                            if(node == rooms.Last) rooms.Last = previousNode;
                            previousNode.Next = next;
                        }
                        rooms.AddFirst(current);
                        break;
                    }
                    previousNode = node;
                    node = next;
                }
            }
            else if (rooms.Count < k)
            {
                rooms.AddFirst(current);
                rooms.Count++;
                count++;
            }
        }
        Console.WriteLine(count);
    }
    private class Activity
    {
        public int Start { get; }
        public int Finish { get; }
        public Activity(int s, int f)
        {
            Start = s;
            Finish = f;
        }
    }

    private class LinkedList
    {
        public LinkedListNode First { get; set; }
        public LinkedListNode Last { get; set; }
        public int Count { get; set; }

        public void AddFirst(Activity a)
        {
            var node = new LinkedListNode(a);
            if (First != null)
            {
                node.Next = First;
                First = node;
            }
            else
            {
                First = node;
                Last = node;
            }
        }
    }

    private class LinkedListNode
    {
        public Activity Value { get; set; }
        public LinkedListNode Next { get; set; }
        public LinkedListNode(Activity a)
        {
            Value = a;
        }
    }
}