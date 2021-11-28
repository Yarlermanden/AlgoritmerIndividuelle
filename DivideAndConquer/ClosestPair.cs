using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DivideAndConquer
{
    public class ClosestPair
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            while (n != 0)
            {
                var list = ReadInput(n);
                var (p1, p2) = DivideAndConquer(list);
                Console.WriteLine(p1.X + " " + p1.Y + " " + p2.X + " " + p2.Y);
                n = int.Parse(Console.ReadLine());
            }
        }

        private static List<Point> ReadInput(int n)
        {
            var list = new List<Point>();
            for (int i = 0; i < n; i++)
            {
                var arr = Console.ReadLine().Split(' ');
                list.Add(new Point(float.Parse(arr[0]), float.Parse(arr[1])));
            }
            return list;
        }

        private static (Point, Point) DivideAndConquer(List<Point> points)
        {
            var pointsSortedX = points.OrderBy(p => p.X).ToList();
            var pointsSortedY = points.OrderBy(p => p.Y).ToList();
            for (int i = 0; i < points.Count; i++) pointsSortedX[i].Id = i;

            var (p1, p2, _) = FindClosestPair(pointsSortedX, pointsSortedY);
            return (p1, p2);
        }

        private static (Point, Point, double) FindClosestPair(List<Point> Px, List<Point> Py)
        {
            if (Py.Count <= 3) return BruteForce(Py);

            var Qx = Px.GetRange(0, Px.Count / 2);
            var Rx = Px.GetRange(Px.Count / 2, Px.Count - Qx.Count);
            var splitX = Qx[Qx.Count - 1];
            var Qy = new List<Point>();
            var Ry = new List<Point>();
            foreach (var p in Py)
            {
                if(p.Id > splitX.Id) Ry.Add(p);
                else Qy.Add(p);
            }

            var (pLeft1, pLeft2, min1) = FindClosestPair(Qx, Qy);
            var (pRight1, pRight2, min2) = FindClosestPair(Rx, Ry);

            var d = min1;
            var p1 = pLeft1;
            var p2 = pLeft2;
            if (min1 > min2)
            {
                d = min2;
                p1 = pRight1;
                p2 = pRight2;
            }

            var maxQx = Qx[Qx.Count - 1].X;
            var minRangeX = maxQx - d;
            var maxRangeX = maxQx + d;

            var s = new List<Point>();
            foreach(var p in Py) if(p.X > minRangeX && p.X <= maxRangeX) s.Add(p);
            for (int i = 0; i < s.Count; i++)
            {
                for (int j = i + 1; j < Math.Min(i + 2, s.Count); j++)
                {
                    var dist = s[i].DistanceTo(s[j]);
                    if (d > dist)
                    {
                        d = dist;
                        p1 = s[i];
                        p2 = s[j];
                    }
                }
            }
            return (p1, p2, d);
        }

        private static (Point, Point, double) BruteForce(List<Point> points)
        {
            double min = float.MaxValue;
            Point p1 = points[0];
            Point p2 = points[1];
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    var dist = points[i].DistanceTo(points[j]);
                    if (min > dist)
                    {
                        min = dist;
                        p1 = points[i];
                        p2 = points[j];
                    }
                }
            }
            return (p1, p2, min);
        }
        
        private class Point
        {
            public float X { get; set; }
            public float Y { get; set; }
            public int Id { get; set; }
            public Point(float x, float y)
            {
                X = x;
                Y = y;
            }

            public double DistanceTo(Point p)
            {
                return Math.Sqrt((X - p.X)*(X-p.X) + (Y - p.Y)*(Y-p.Y));
            }
        }
    }
}