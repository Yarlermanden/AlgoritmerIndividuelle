using System;
using System.Linq;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class WateringGrass
    {
        public static void Main()
        {
            while (true)
            {
                var s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s)) break;
                var arr = s.Split(' ').Select(x => int.Parse(x)).ToArray();
                int n = arr[0];
                int l = arr[1];
                double w = arr[2] / 2.0;
                w = w * w;
                var sprinklers = new Sprinkler[n];
                for (int i = 0; i < n; i++)
                {
                    arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    sprinklers[i] = FindStartAndEnd(arr[0], arr[1], w);
                }
                Array.Sort(sprinklers, (a, b) => a.Start.CompareTo(b.Start));

                int count = 0;
                double currentEnd = 0;
                double candidateEnd = 0;
                foreach (var sp in sprinklers)
                {
                    if(Double.IsNaN(sp.Start)) continue;

                    if (sp.Start > currentEnd)
                    {
                        if (sp.Start <= candidateEnd)
                        {
                            currentEnd = candidateEnd;
                            count++;
                            if (sp.End > currentEnd) candidateEnd = sp.End;
                            if (candidateEnd >= l)
                            {
                                count++;
                                break;
                            }
                        }
                        else
                        {
                            count = -1;
                            break;
                        }
                    }
                    else
                    {
                        if (sp.End > candidateEnd)
                        {
                            candidateEnd = sp.End;
                            if (candidateEnd >= l)
                            {
                                count++;
                                break;
                            }
                        }
                    }
                }

                if (candidateEnd < l) count = -1;
                Console.WriteLine(count);
            }
        }

        private static Sprinkler FindStartAndEnd(int x, int r, double w)
        {
            var p = Math.Sqrt(r * r - w);
            return new Sprinkler(x - p, x + p);
        }

        public class Sprinkler
        {
            public double Start { get; set; }
            public double End { get; set; }
            
            public Sprinkler(double start, double end)
            {
                Start = start;
                End = end;
            }
        }
    }
}
