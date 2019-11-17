using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectEulerProblemFive
{
    public static class EvenlyDivisibleEvaluator
    {
        public static int DivisibleByAllNumbersUpTo(int n)
        {
            IDictionary<int, int> factorsUpToN = GetFactorsUpTo(n);

            int result = 1;

            foreach (var factor in factorsUpToN)
            {
                for (int i = 0; i < factor.Value; ++i)
                {
                    result *= factor.Key;
                }
            }

            return result;
        }

        private static IDictionary<int, int> GetFactorsUpTo(int n)
        {
            IEnumerable<int> primesUpToN = PrimeFinder.GetPrimesUpTo(n);

            IDictionary<int, int> factorsUpToN = new Dictionary<int, int>();
            
            foreach (int prime in primesUpToN)
            {
                int countOfPrimeForFactors = 1;
                int newPrime = prime;

                while (newPrime <= n)
                {
                    newPrime *= prime;
                    ++countOfPrimeForFactors;
                }
                
                factorsUpToN[prime] = countOfPrimeForFactors - 1;
            }

            return factorsUpToN;
        }
    }
}
