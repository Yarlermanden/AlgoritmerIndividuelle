using System;
using System.Collections.Generic;
using System.Linq;

namespace DivideAndConquer
{
    public class MergeSort
    {
        public static void Main1()
        {
            var array = new int[] {1, 5, 2, 4, 3, 7, 8, 9, 3, 1, 2};
            array = Sort(array);
            foreach (var b in array)
            {
                Console.WriteLine(b);
            }
        }

        private static int[] Sort(int[] arr)
        {
            if (arr.Length == 1) return arr;
            //Divide
            var leftArr = new int[arr.Length / 2];
            Array.Copy(arr, 0, leftArr, 0, arr.Length/2);
            var rightArr = new int[arr.Length - (arr.Length/2)];
            Array.Copy(arr, arr.Length/2, rightArr, 0, arr.Length - (arr.Length/2));
            
            //Conquer
            leftArr = Sort(leftArr);
            rightArr = Sort(rightArr);
            
            //Combine
            int i = 0;
            int j = 0;
            for (int k = 0; k < arr.Length; k++)
            {
                if (i == leftArr.Length || (j != rightArr.Length && leftArr[i] > rightArr[j]))
                {
                    arr[k] = rightArr[j];
                    j++;
                }
                else
                {
                    arr[k] = leftArr[i];
                    i++;
                }
            }
            return arr;
        }
    }
}