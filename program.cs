using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePrime
{
    class Program
    {
        static void Main(string[] args)
        {
            Find12DigitProductionOfPrimeSet();
        }

        
        static void Find12DigitProductionOfPrimeSet()
        {
            var list = GeneratePrimeList();
            var c = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (i % 10 == 0) Console.WriteLine();
                Console.Write("{0}, ", list[i]);

            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Total Prime Numbers under 1000: {0}", list.Count);
            Console.WriteLine();

            var tuples = GetTuple(list);
            // Display the result.          
            Console.WriteLine("{0,3} {1,3} {2,3} {3,3} {4,12}\n",
                              "No1", "No2", "No3", "No4", "12-digit-result");
            for (int i = 0; i < tuples.Count; i++)
            {
                Console.WriteLine("{0,3} {1,3} {2,3} {3,3} {4,12}\n",
                              tuples[i].Item1,tuples[i].Item2,tuples[i].Item3,tuples[i].Item4,tuples[i].Item5);
            }

            Console.WriteLine("Press any key to end...");
            Console.ReadKey();
        }

        public static List<Tuple<int, int, int, int, long>> GetTuple(List<int> primes)
        {
            var list = new List<Tuple<int, int, int, int, long>>();
            Tuple<int, int, int, int, long> tuple;

            var array = new int[primes.Count];
            primes.CopyTo(array);
            var copy = array.ToList();

            for (int i = 0; i < primes.Count - 3; i++)
            {
                for (int j = i + 1; j < primes.Count - 2; j++)
                {
                    for (int k = j + 1; k < primes.Count - 1; k++)
                    {
                        for (int m = k + 1; m < primes.Count; m++)
                        {
                            long result = 1;
                            result = result*primes[i]*primes[j]*primes[k]*primes[m];
                            if (IsDigitOk(result))
                            {
                                tuple = new Tuple<int, int, int, int, long>(primes[i], primes[j], primes[k], primes[m], result);
                                list.Add(tuple);
                            }
                        }
                    }
                }
            }

            return list;
        } 

        public static bool IsDigitOk(long num)
        {
            if (IsDigitLess(num)) return false;
            if (IsDigitMore(num)) return false;

            var digits = num.ToString().Select(c => byte.Parse(c.ToString())).ToList();
            for (int i = 1; i < digits.Count; i++)
            {
                if (Math.Abs(digits[i - 1] - digits[i]) > 1) return false;
            }

            return true;
        }

        public static bool IsDigitLess(long num)
        {
            return 100000000000 > num;
        }

        public static bool IsDigitMore(long num)
        {
            return num > 999999999999;
        }

        public static List<int> GeneratePrimeList()
        {
            var list = new List<int>();
            
            for (int n = 2; n < 1000; n++)
            {
                if (IsPrimality(n)) list.Add(n);
            }

            return list;
        }

        public static bool IsPrimalitySlow(int n)
        {
            int i = 2;
            while (i * i <= n)
            {
                if (n % i == 0) return false;
                i++;
            }
            return true;
        }

        public static bool IsPrimality(int n)
        {
            if (n <= 1) return false;
            if (n <= 3) return true;
            if (n%2 == 0 || n%3 == 0) return false;

            int i = 5;
            while (i * i <= n)
            {
                if (n%i == 0 || n%(i + 2) == 0) return false;
                i += 6;
            }

            return true;
        }
    }
}
