using System;
using System.Collections.Generic;
using System.Linq;

public class RestaurantOrders
{
    private static int[,] arr2D;
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var menu = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        int ordersPlaced = int.Parse(Console.ReadLine());
        var costs = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
        int maxCost = costs.Max();

        arr2D = new int[maxCost + menu.Max()+1, menu.Length];
        for (int j = 0; j < menu.Length; j++)
        {
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
                        if (arr2D[i + menu[k], k] != 0)
                        {
                            //Tjek om de her to er ens
                            var list1 = BackTrack(i + menu[k], k, menu);
                            var list2 = BackTrack(i, j, menu);
                            list2.Add(j+1);
                            //list1.OrderBy(x => x).Intersect(list2.OrderBy(x => x));
                            //list1.SequenceEqualsIgnoreOrder
                        }
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
            List<int> knownItems = new List<int>();
            for (int j = 0; j < menu.Length; j++)
            {
                if (multiple) break;
                if (arr2D[order, j] != 0)
                {
                    if (!exist)
                    {
                        exist = true;
                        knownItems = BackTrack(order, j + 1, menu);
                        knownItems = knownItems.OrderBy(x => x).ToList();
                        //Console.WriteLine(knownItems[0] + " " + knownItems[1] + " " + knownItems[2] + " " + knownItems[3]);
                    }
                    else
                    {
                        var list1 = BackTrack(order, j + 1, menu);
                        //Console.WriteLine(list1[0] + " " + list1[1] + " " + list1[2] + " " + list1[3]);
                        if (list1.Count != knownItems.Count)
                        {
                            multiple = true;
                            break;
                        }
                        list1 = list1.OrderBy(x => x).ToList();
                        for (int k = 0; k < list1.Count; k++)
                        {
                            if (knownItems[k] != list1[k])
                            {
                                multiple = true;
                                break;
                            }
                        }
                    }
                }
            }
            
            if(!exist) Console.WriteLine("Impossible");
            else if(multiple) Console.WriteLine("Ambiguous");
            else
            {
                Console.Write(knownItems[0]);
                for(int i = 1; i < knownItems.Count; i++) Console.Write(" " + knownItems[i]);
                Console.WriteLine();
                //string s = BackTrack(order, menuItem, menu);
                //Console.WriteLine(s);
            }
        }
        
        //Iterate through the array
        
        //For each column in a row. Find all the possible costs adding the cost of the orders and input the value at the column of the order at the cost-row
        //If an order matches the price of any of the costs given - add the path to this order to something, which allows to check later
    }

    //private static string BackTrack(int i, int j, int menuCost)
    private static List<int> BackTrack(int i, int j, int[] menu)
    {
        //if (i == 0) return j.ToString();
        if (i == 0) return new List<int>();
        //int oldJ = arr2D[i, j];
        //BackTrack(arr2D[i - menuCost, oldJ];
        //return j + " " + BackTrack(i-menuCost, arr2D[i,j], )
        //return j + " " + BackTrack(i - menu[j-1], arr2D[i, j-1], menu);
        var list = BackTrack(i - menu[j - 1], arr2D[i, j - 1], menu);
        list.Add(j);
        return list;
    }
}