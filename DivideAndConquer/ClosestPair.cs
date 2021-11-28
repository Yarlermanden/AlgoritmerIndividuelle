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

        private static Point[] ReadInput(int n)
        {
            var list = new Point[n];
            for (int i = 0; i < n; i++)
            {
                var arr = Console.ReadLine().Split(' ');
                list[i] = new Point(double.Parse(arr[0]), double.Parse(arr[1]));
            }
            return list;
        }

        private static (Point, Point) DivideAndConquer(Point[] points)
        {
            var pointsSortedX = new Point[points.Length];
            var pointsSortedY = new Point[points.Length];
            Array.Copy(points, pointsSortedX, points.Length);
            Array.Copy(points, pointsSortedY, points.Length);
            Array.Sort(pointsSortedX, (a, b) => (!a.X.Equals(b.X)) ? a.X.CompareTo(b.X) : a.Y.CompareTo(b.Y));
            Array.Sort(pointsSortedY, (a, b) => (!a.Y.Equals(b.Y)) ? a.Y.CompareTo(b.Y) : a.X.CompareTo(b.X));

            var (p1, p2, _) = FindClosestPair(pointsSortedX, pointsSortedY);
            return (p1, p2);
        }

        private static (Point, Point, double) FindClosestPair(Point[] Px, Point[] Py)
        {
            if (Py.Length <= 3) return BruteForce(Py);

            var Qx = new Point[Px.Length/2];
            var Rx = new Point[Px.Length - Qx.Length];
            Array.Copy(Px, Qx, Qx.Length);
            Array.Copy(Px, Px.Length/2, Rx, 0, Rx.Length);
            var Qy = new Point[Qx.Length];
            var Ry = new Point[Rx.Length];
            int qi = 0, ri = 0;
            Point midPoint = Qx[Qx.Length - 1];
            foreach(var p in Py)
            {
                if (p.X > midPoint.X || ((p.X.Equals(midPoint.X) && p.Y > midPoint.Y) && ri < Rx.Length) || qi == Qx.Length)
                {
                    Ry[ri++] = p;
                }
                else Qy[qi++] = p;
            }

            var (pLeft1, pLeft2, min1) = FindClosestPair(Qx, Qy);
            var (pRight1, pRight2, min2) = FindClosestPair(Rx, Ry);

            double d;
            Point p1;
            Point p2;
            if (min1 > min2)
            {
                d = min2;
                p1 = pRight1;
                p2 = pRight2;
            }
            else
            {
                d = min1;
                p1 = pLeft1;
                p2 = pLeft2;
            }

            var s = new List<Point>();
            foreach(var p in Py) if(Math.Abs(p.X - midPoint.X) < d) s.Add(p);
            double dist = 0;
            for (int i = 0; i < s.Count; i++)
            {
                for (int j = i + 1; j < s.Count && (s[j].Y - s[i].Y < d); j++)
                {
                    dist = s[i].DistanceTo(s[j]);
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

        private static (Point, Point, double) BruteForce(Point[] points)
        {
            double min = float.MaxValue;
            Point p1 = points[0];
            Point p2 = points[1];
            int n = points.Length;
            double dist = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    dist = points[i].DistanceTo(points[j]);
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
            public double X { get; set; }
            public double Y { get; set; }
            public Point(double x, double y)
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