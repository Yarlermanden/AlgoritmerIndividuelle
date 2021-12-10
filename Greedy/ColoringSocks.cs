using System;
using System.Linq;

public class ColoringSocks
{
    public static void Main()
    {
        var arr = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        //int s = int.Parse(arr[0]); //socks
        //int c = int.Parse(arr[1]); //Max socks in one machine
        //int k = int.Parse(arr[2]); //color difference
        var socks = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        Array.Sort(socks, (x, y) => x.CompareTo(y));
        int machineCount = 1;
        int currentMachineCount = 0;
        int smallestSock = socks[0];
        foreach(var sock in socks)
        {
            if (sock - smallestSock > arr[2] || currentMachineCount == arr[1]) //new machine
            {
                machineCount++;
                currentMachineCount = 1;
                smallestSock = sock;
            }
            else //add suck to current machine
            {
                currentMachineCount++;
            }
        }
        Console.WriteLine(machineCount);
    }
}