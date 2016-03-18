using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Cut_Rod_Problem
{
    class Program
    {
        private int simpleCutRod(int [] p, int n)
        {
            if (n == 0)
                return 0;
            int q = int.MinValue;

            for (int i = 1; i <= n; i++)
                q = Math.Max(q, p[i] + simpleCutRod(p, n - i));

            return q;
        }

        int memoizedCutRodAux(int [] p, int n, int [] r)
        {
            if (r[n] >= 0)
                return r[n];
            int q=0;
            if (n == 0)
                q = 0;
            else
            {
                q = int.MinValue;
                for (int i = 1; i <= n; i++)
                    q = Math.Max(q, p[i] + memoizedCutRodAux(p, n - i, r));
            }
            r[n] = q;
            return q;
                   
        }

        int memoizedCutRod(int [] p, int n)
        {
            int[] r = new int[n + 1];
            for (int i = 0; i <= n; i++)
                r[i] = int.MinValue;

            return memoizedCutRodAux(p, n, r);
        }

        int bottomUpCutRod(int [] p, int n)
        {
            int[] r = new int[n + 1]; 
            
            for (int j = 1; j <= n; j++)
            {
                int q = int.MinValue;
                for (int i = 1; i <= j; i++)
                    q = Math.Max(q, p[i] + r[j - i]);
                r[j] = q;
            }

            return r[n]; 
        }

        static void Main(string[] args)
        {
            int n = 0;
            Console.WriteLine("Enter number of Inches : ");
            n = int.Parse(Console.ReadLine());

            string fileContent = File.ReadAllText("Data.txt");
            string[] priceData = fileContent.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] prices = new int[priceData.Length+1];

            for (int i = 1; i < prices.Length; i++)
                prices[i] = int.Parse(priceData[i-1]);

            
            Stopwatch t = new Stopwatch();
            t.Start();

            Cut_Rod_Problem.Program ctp = new Program();

            int bestPrice = ctp.memoizedCutRod(prices, n);
            //int bestPrice = ctp.bottomUpCutRod(prices, n);
            //int bestPrice = ctp.simpleCutRod(prices, n);

            t.Stop();
            
            Console.WriteLine("Best Revenue is upto : " + bestPrice);
            Console.WriteLine("Time Elapsed : " + t.Elapsed);
        }
    }
}
