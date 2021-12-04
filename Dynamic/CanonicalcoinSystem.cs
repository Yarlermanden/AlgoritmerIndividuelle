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
            
            int maxToCheck = arr[arr.Length-1] + arr[arr.Length-2];
            var canonical = IsItCanonical(arr, maxToCheck);

            if(!canonical) Console.WriteLine("non-canonical");
            else Console.WriteLine("canonical");
        }

        private static bool IsItCanonical(int[] coinArr, int max)
        {
            int?[] greedyArr = new int?[max];
            int?[] dynamicArr = new int?[max];

            greedyArr[0] = 0;
            dynamicArr[0] = 0;

            int? nextCoinCount;
            for (int i = 0; i < max; i++)
            {
                int? currentCoinCountGreedy = greedyArr[i];
                int? currentCoinCountDynamic = dynamicArr[i];
                if (currentCoinCountDynamic != currentCoinCountGreedy) return false;
                
                foreach (var coin in coinArr)
                {
                    if(i + coin >= max) continue;
                    nextCoinCount = greedyArr[i + coin];
                    if (nextCoinCount == null) greedyArr[i + coin] = currentCoinCountGreedy + 1;
                    
                    nextCoinCount = dynamicArr[i + coin];
                    if (nextCoinCount == null) dynamicArr[i + coin] = currentCoinCountDynamic + 1;
                    else
                    {
                        if (currentCoinCountDynamic + 1 < nextCoinCount) dynamicArr[i+coin] = currentCoinCountDynamic + 1;
                    }
                }
            }
            return true;
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
