using System;
using System.Linq;
using System.Collections.Generic;

namespace ProjectEulerProblemFour
{
    public static class PalindromeEvaluator
    {
        public static int LargestPalindromeFromNumbersOfLengthN(int n)
            {
            int lowerBound = (int)(Math.Pow(10, (double)(n-1)));
            int count = (int)(Math.Pow(10, (double)n)) - lowerBound;
            IEnumerable<int> numbersOfLengthN = Enumerable.Range(lowerBound, count);

            IEnumerable<int> palindromes = numbersOfLengthN.SelectMany(number1 =>
                numbersOfLengthN.Select(number2 =>
                    number1 * number2)).Where(number => IsPalindrome(number));

            return palindromes.Max();
        }

        private static bool IsPalindrome(int n)
        {
            return n == ReverseNumber(n);
        }

        private static int ReverseNumber(int n)
        {
            int reversedNumber = 0;

	    while (n > 0)
            {
                reversedNumber *= 10;
                reversedNumber += (n%10);
                n /= 10;
            }

            return reversedNumber;
        }
    }
}
