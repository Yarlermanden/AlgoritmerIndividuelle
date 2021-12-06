using System;
using System.Linq;

namespace Dynamic
{
    public class CanonicalcoinSystem
    {
        public static void Main1()
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
            int[] arr = new int[max];
            arr[0] = 0;

            int nextCoinCount;
            for (int i = 0; i < max; i++)
            {
                int currentCoinCountDynamic = arr[i];
                
                foreach (var coin in coinArr)
                {
                    if(i + coin >= max) continue;
                    nextCoinCount = arr[i + coin];
                    if (nextCoinCount == 0) arr[i + coin] = currentCoinCountDynamic + 1;
                    else if (nextCoinCount > currentCoinCountDynamic + 1) return false;
                }
            }
            return true;
        }
    }
}
