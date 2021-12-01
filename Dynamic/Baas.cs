using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace Dynamic
{
    public class Baas
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int?[,,] arr3D = new int?[n, n, n];
            List<int>[] dependsOn = new List<int>[n];

            int[] jobTime = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();

            for (int i = 0; i < n; i++)
            {
                var list = Console.ReadLine().Split(' ').Skip(1).Select(x => int.Parse(x)-1).ToList();
                dependsOn[i] = list;
            }

            for (int k = 0; k < n; k++)
            {
                var realJobTime = jobTime[k];
                jobTime[k] = 0;
                arr3D[k, 0, 0] = jobTime[0];
                for (int i = 1; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (arr3D[k, i-1, j] != null)
                        {
                            arr3D[k, i, j] = arr3D[k, i - 1, j];
                            continue;
                        }

                        int slowestPrev = -1;
                        foreach (var x in dependsOn[j])
                        {
                            var prev = arr3D[k, i - 1, x];
                            if (prev == null) break;
                            if (prev > slowestPrev) slowestPrev = (int) prev;
                        }

                        if (slowestPrev == -1) continue;
                        arr3D[k, i, j] = slowestPrev + jobTime[j];
                    }
                }
                jobTime[k] = realJobTime;
            }
            int min = Int32.MaxValue;
            for(int k = 0; k < n; k++)
                if (arr3D[k, n - 1, n - 1] < min)
                    min = (int) arr3D[k, n - 1, n - 1];
            Console.WriteLine(min);
        }
    }
}




//Hver column representerer et job
//Hver row representere endnu et job startes/færdiggøres
//Ved row 1 startes første job
//Ved row 2 startes alle, som kun depender på job 1
//Ved row 3 startes alle, som ikke depender på nogen, som ikke allerede er kort
//Hver column representere det tidligste tidspunkt dette job kan være færdigt
//Hver column bærer sin værdi med ned hver gang
//Hver column tager den langsommeste af alle dem, som den venter på og lægger derefter sin egen tid oveni