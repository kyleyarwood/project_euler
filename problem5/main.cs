using System;

namespace ProjectEulerProblemFive
{
    public class Program
    {
        public static void Main()
        {
            const int problemValue = 20;
            int result = EvenlyDivisibleEvaluator
                .DivisibleByAllNumbersUpTo(problemValue);
            Console.WriteLine(result);
        }
    }
}
