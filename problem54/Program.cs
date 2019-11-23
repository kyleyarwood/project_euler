using System;
using System.Linq;
using System.Collections.Generic;
using Poker;

public class Program 
{
    public static void Main()
    {
        IEnumerable<Match> matches = 
	    MatchReader.ReadHandsIntoMatches("Poker/poker.txt");
	int numMatchesPlayerOneWins =
            matches.Sum(match => 
		MatchEvaluator.DoesPlayerOneWin(match) ? 1 : 0);
        Console.WriteLine(numMatchesPlayerOneWins);
    }
}
