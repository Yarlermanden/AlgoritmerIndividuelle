using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dynamic
{
    public class NestedDolls
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++) NestedDoll();
        }

        private static void NestedDoll()
        {
            int n = int.Parse(Console.ReadLine());

            var sb = new StringBuilder();
            while (true)
            {
                char ch = Convert.ToChar(Console.Read());
                if (ch == '\n') break;
                sb.Append(ch);
            }

            var arr = sb.ToString().Split(' ').Select(x => int.Parse(x)).ToArray();
            var dolls = new Doll[n];
            for (int i = 0; i < n * 2; i += 2)
            {
                var w = arr[i];
                var h = arr[i+1];
                dolls[i / 2] = new Doll(w, h);
            }
            
            //Maybe it would be nice to sort by X and Y to ensure we either get all the biggest or smallest dolls first
            
            //int result = SolveDynamically(dolls);
            //Console.WriteLine(result);
            dolls = dolls.OrderBy(x => x.W).OrderBy(x => x.H).ToArray();

            while (dolls.Length > 0)
            {
                dolls = Iterate(dolls);
            }
        }

        private static Doll[] Iterate(Doll[] dolls)
        {
            int n = dolls.Length;
            Doll[,] arr2D = new Doll[n, n];
            var tempDoll = new Doll(dolls[0]);
            arr2D[0, 0] = tempDoll;

            for (int i = 1; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    //Check all dolls < j at i-1 for which can fit doll j and which has largest count
                    //Check if dolls[j] can fit inside arr2d[
                }
            }
        }

        private static int SolveDynamically(Doll[] dolls)
        {
            var n = dolls.Length;
            List<Doll>[][] arr2D = new List<Doll>[n][];
            for(int i = 0; i < n; i++) arr2D[i] = new List<Doll>[n];
            
            //foreach row try to pack another doll either inside or outside the current or increase the number of dolls
            for (int j = 0; j < n; j++) arr2D[0][j] = new List<Doll>() {dolls[0]};
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(i==j) continue;
                    //try to add to each of the columns below
                    
                    //alternatively try to add doll[i] to each of the column [i][j] from [i-1][j]
                }
            }
                
                
                
            //foreach row we try to wrap doll[i] into each of the dolls at column[j] - except for i == j
            //This is done by checking whether doll[i] is either larger both in w and h or smaller in w and h than each of the dolls from the previous. If yes combine else add to list
            return 0;
        }

        public class Doll
        {
            public int W { get; set; }
            public int H { get; set; }
            public int NestedCount { get; set; }
            public Doll Inside { get; set; }
            public Doll(int w, int h)
            {
                W = w;
                H = h;
            }

            public Doll(Doll doll)
            {
                W = doll.W;
                H = doll.H;
            }
        }
    }
}