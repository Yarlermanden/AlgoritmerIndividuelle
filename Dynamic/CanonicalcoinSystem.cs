using System;
using System.Linq;

namespace Dynamic
{
    public class CanonicalcoinSystem
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            
            //sum of the two largest denominations
            int maxToCheck = arr[arr.Length-1] + arr[arr.Length-2] + 1;

            bool canonical = true;
            var dynamicArr = Dynamic(arr, maxToCheck);
            var greedyArr = DynamicGreedy(arr, maxToCheck);
            for (int i = 1; i < maxToCheck; i++)
            {
                //int greedy = Greedy(arr, i);
                int dynamic = (int)dynamicArr[i];
                int greedy = (int)greedyArr[i];

                //Console.WriteLine(greedy + ", " + dynamic);

                if (greedy != dynamic) canonical = false;
            }

            if(!canonical) Console.WriteLine("non-canonical");
            else Console.WriteLine("canonical");
        }

        private static int?[] DynamicGreedy(int[] coinArr, int max)
        {
            int?[] arr = new int?[max];

            arr[0] = 0;
            for (int i = 0; i < max; i++)
            {
                int? currentCoinCount = arr[i];
                foreach (var coin in coinArr)
                {
                    if(i + coin >= max) continue;
                    int? nextCoinCount = arr[i + coin];
                    if (nextCoinCount == null) arr[i + coin] = currentCoinCount + 1;
                }
            }
            return arr; 
        }

        private static int Greedy(int[] arr, int max)
        {
            int count = 0;
            int rest = max;
            int coin = arr.Length-1;
            while (rest > 0)
            {
                while (rest < arr[coin])
                {
                    coin--;
                }
                rest = rest - arr[coin];
                count++;
            }
            return count;
        }

        private static int?[] Dynamic(int[] coinArr, int max)
        {
            int?[] arr = new int?[max];

            arr[0] = 0;
            for (int i = 0; i < max; i++)
            {
                int? currentCoinCount = arr[i];
                foreach (var coin in coinArr)
                {
                    if(i + coin >= max) continue;
                    int? nextCoinCount = arr[i + coin];
                    if (nextCoinCount == null) arr[i + coin] = currentCoinCount + 1;
                    else
                    {
                        if (currentCoinCount + 1 < nextCoinCount) arr[i+coin] = currentCoinCount + 1;
                    }
                }
            }
            return arr;
        }
    }
}
