using System;

namespace ProjectEulerProblemFour
{
    public class Program
    {
        public static void Main() {
            const int problemValue = 3;
	    int result = PalindromeEvaluator
                .LargestPalindromeFromNumbersOfLengthN(problemValue);
            Console.WriteLine(result);
        }
    }
}
