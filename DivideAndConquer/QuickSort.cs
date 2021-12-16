namespace DivideAndConquer;

public class QuickSort
{
    public static void Main()
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
        if (arr.Length < 2) return arr;
        //Divide
        //Choose random pivot
        int pivot = arr[0];

        var leftList = new List<int>();
        var rightList = new List<int>();
        var middleList = new List<int>();

        for (int i = 0; i < arr.Length; i++)
        {
            if(arr[i] < pivot) leftList.Add(arr[i]);
            else if (arr[i] > pivot) rightList.Add(arr[i]);
            else middleList.Add(arr[i]);
        }

        //Conquer
        int[] leftArr = Sort(leftList.ToArray());
        int[] rightArr = Sort(rightList.ToArray());

        //Combine
        for (int i = 0; i < leftArr.Length; i++) arr[i] = leftArr[i];
        for (int i = 0; i < middleList.Count; i++) arr[i + leftArr.Length] = middleList[i];
        for (int i = 0; i < rightArr.Length; i++) arr[i + leftArr.Length + middleList.Count] = rightArr[i];
        return arr;
    }
}