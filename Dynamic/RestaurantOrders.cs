using System;
using System.Linq;

public class RestaurantOrders
{
    private static int[,] arr2D;
    public static void Main()
    {
        //Make a 2d array which is a wide as the amount of orders and as deep as the max amount of turns
        int n = int.Parse(Console.ReadLine());
        var menu = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        int ordersPlaced = int.Parse(Console.ReadLine());
        var costs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        int minOrder = menu.Min();
        int maxCost = costs.Max();

        //int maxTurns = maxCost / minOrder + 1;
        arr2D = new int[maxCost + menu.Max()+1, menu.Length];
        for (int j = 0; j < menu.Length; j++)
        {
            //initialize
            arr2D[menu[j], j] = j+1;
        }

        for (int i = 0; i < maxCost; i++)
        {
            for (int j = 0; j < menu.Length; j++)
            {
                if (arr2D[i, j] != 0)
                {
                    for(int k = 0; k < menu.Length; k++)
                    {
                        arr2D[i + menu[k], k] = j+1;
                    }
                }
            }
        }

        foreach (var order in costs)
        {
            bool exist = false;
            bool multiple = false;
            int menuItem = 0;
            for (int j = 0; j < menu.Length; j++)
            {
                if (arr2D[order, j] != 0)
                {
                    //string s = BackTrack(order, j, menu[j]);
                    string s = BackTrack(order, j+1, menu);
                    Console.WriteLine(s);
                    if (!exist)
                    {
                        exist = true;
                        menuItem = j;
                    }
                    else multiple = true;
                }
            }
            
            if(!exist) Console.WriteLine("Impossible");
            else if(multiple) Console.WriteLine("Ambiguous");
            else
            {
                //string s = BackTrack(order, menuItem, menu);
                //Console.WriteLine(s);
            }
        }
        
        //Iterate through the array
        
        //For each column in a row. Find all the possible costs adding the cost of the orders and input the value at the column of the order at the cost-row
        //If an order matches the price of any of the costs given - add the path to this order to something, which allows to check later
    }

    //private static string BackTrack(int i, int j, int menuCost)
    private static string BackTrack(int i, int j, int[] menu)
    {
        //if (i == 0) return j.ToString();
        if (i == 0) return "";
        //int oldJ = arr2D[i, j];
        //BackTrack(arr2D[i - menuCost, oldJ];
        //return j + " " + BackTrack(i-menuCost, arr2D[i,j], )
        return j + " " + BackTrack(i - menu[j-1], arr2D[i, j-1], menu);
    }
}