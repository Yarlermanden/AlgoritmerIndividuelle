using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace Dynamic
{
    public class Baas
    {
        public static void Main1()
        {
            int n = int.Parse(Console.ReadLine());
            int[,] arr2D = new int[n,n];
            List<int>[] dependsOn = new List<int>[n];

            int[] jobTime = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            for (int i = 0; i < n; i++)
            {
                var list = Console.ReadLine().Split(' ').Skip(1).Select(x => int.Parse(x)-1).ToList();
                dependsOn[i] = list;
            }
            
            arr2D[0, 0] = 0;
            for (int j = 1; j < n; j++) arr2D[0, j] = jobTime[0];

            int currentJobTime;
            int latestStartTime;
            int prev;
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    currentJobTime = i == j ? 0 : jobTime[i];
                    latestStartTime = 0;
                    foreach (var x in dependsOn[i])
                    {
                        prev = arr2D[x, j];
                        if(prev > latestStartTime) latestStartTime = prev;
                    }
                    arr2D[i, j] = latestStartTime + currentJobTime;
                }
            }
            int min = Int32.MaxValue;
            for(int j = 0; j < n; j++)
                if (arr2D[n-1,j] < min)
                    min = arr2D[n-1,j];
            Console.WriteLine(min);
        }
    }
}