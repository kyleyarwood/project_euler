using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectEulerProblemFive
{
    public static class PrimeFinder
    {
        public static IEnumerable<int> GetPrimesUpTo(int n)
        {
            List<int> primes = new List<int>();
	    
	    if (n >= 2)
            {
                primes.Add(2);
            }
	    
       	    for (int i = 3; i <= n; i += 2)
            {
                int bound = (int)(Math.Sqrt(i));

                foreach (int prime in primes)
                {
                    if (prime > bound)
                    {
                        primes.Add(i);
			break;
                    }
		    else if (i%prime == 0)
                    {
                        break;
                    }
                }
            }

	    return primes;
        }
    }
}
