using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

namespace Dynamic;

public class PebbleSolitaire
{
    public static void Main1()
    {
        int n = int.Parse(Console.ReadLine());
        int[] binary = new int[23];
        for (int i = 0; i < 23; i++) binary[i] = 1 << i;
        var dicArr = BuildUp(binary);
        for (int i = 0; i < n; i++)
        {
            var s = Console.ReadLine();
            int val = 0;
            int pieces = 0;
            for (int j = 0; j < 23; j++)
            {
                if (s[j] == 'o')
                {
                    val += binary[j];
                    pieces++;
                }
            }
            Console.WriteLine(val);
            dicArr[pieces-1].TryGetValue(val, out var x);
            Console.WriteLine(x);
        }
    }

    private static Dictionary<int, int>[] BuildUp(int[] binary)
    {
        Dictionary<int, int>[] dicArr = new Dictionary<int, int>[23];

        for (int i = 0; i < 23; i++) dicArr[i] = new Dictionary<int, int>();

        int v;
        for (int i = 1; i < 8388608; i++)
        {
            v = i;
            //v = v - ((v>>1) & 0x55555555);
            v = v - ((v<<1) & 0x55555555);
            //v = (v & 0x33333333) + ((v>>2) & 0x33333333);
            v = (v & 0x33333333) + ((v<<2) & 0x33333333);
            //v = ((v + (v>>4) & 0xF0F0F0F) * 0x1010101) >> 24;
            v = ((v + (v<<4) & 0xF0F0F0F) * 0x1010101) << 24;
            dicArr[v-1][i] = v-1;
        }
        
        
        //for (int j = 0; j < 23; j++) dicArr[0].Add(binary[j], 1);
        //for (int j = 0; j < 23; j++) dic.Add(binary[j], 1);
        for (int i = 1; i < 23; i++) //amount of pieces
        {
            //for (int j = 0; j < 23; j++) //Combinations with this amount of pieces
            
            //Add all the possible values generated from strings of this number i
            
            
            foreach(var j in dicArr[i-1].Keys)
            {
                for (int k = 0; k < 23; k++) //places to check for this binary number
                {
                    //var possibleToAdd = (binary[j] & j) == 0 && (binary[j + 1] & j) == 0;
                    bool possible = false;
                    if (k > 1 && (binary[k] & j) != 0 && (binary[k - 1] & j) == 0 && (binary[k-2] & j) == 0)
                    {
                        //possible to add after
                        //dic.TryAdd(j - binary[k] + binary[k - 1] + binary[k - 2], i);
                        dicArr[i][j - binary[k] + binary[k - 1] + binary[k - 2]] = dicArr[i-1][j];
                        possible = true;
                    }
                    if (k < 21 && (binary[k] & j) != 0 && (binary[k + 1] & j) == 0 && (binary[k+2] & j) == 0)
                    {
                        //possible to add before
                        //dic.TryAdd(j - binary[k] + binary[k + 1] + binary[k + 2], i);
                        dicArr[i][j - binary[k] + binary[k + 1] + binary[k + 2]] = dicArr[i-1][j];
                    }
                    //We need to add all of those with this same amount of 'o's but which haven't been added yet
                }
            }
            
        }
        
        /*
        Console.WriteLine("dic");
        for (int i = 0; i < 23; i++)
        {
            Console.WriteLine("i: " + i);
            foreach (var x in dicArr[i].Keys)
            {
                Console.WriteLine(x);
            }
        }
        */
        
        return dicArr;
    }
    
    private static int Dynamic(string s)
    {
        char[,,][] arr3D = new char[2,23,23][];
        var arr = s.ToCharArray();

        for (int j = 0; j < 23; j++)
        {
            arr3D[0, 0, j] = arr;
            arr3D[1, 0, j] = arr;
        }

        int rounds = 0;
        for (int i = 0; i < 22; i++)
        {
            //validate a string:
            //A block - doesn't contain -- and contains at least one oo
            //How many blocks do the string have?

            for (int j = 0; j < 23; j++)
            {
                var temp = arr3D[0, i, j];
                if(temp == null) continue;

                for (int k = 0; k < 22; k++)
                {
                    if (temp[k] == 'o' && temp[k + 1] == 'o' )
                    {
                        if (k < 21 && temp[k+2] == '-')
                        {
                            //jump forward
                            //if (!string.IsNullOrEmpty(arr3D[0, i + 1, k + 2]))
                            if(arr3D[0,i+1,k+2] == null)
                            {
                                rounds = i+1;
                                var nextTemp = temp;
                                nextTemp[k] = '-';
                                nextTemp[k+1] = '-';
                                nextTemp[k+2] = 'o';
                                arr3D[0, i + 1, k + 2] = nextTemp;
                            }
                        }

                        if (k > 0 && temp[k-1] == '-')
                        {
                            if (arr3D[0, i + 1, k - 1] == null)
                            {
                                rounds = i+1;
                                var nextTemp = temp;
                                nextTemp[k] = '-';
                                nextTemp[k + 1] = '-';
                                nextTemp[k - 1] = 'o';
                            }
                            //jump backwards
                        }
                    }
                }
            }
        }
        return s.Select(x => x == 'o' ? 1 : 0).Sum()-rounds;
    }
}