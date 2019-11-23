using System;
using System.Linq;
using System.Collections.Generic;
using Poker;
using PokerTests;

public class Program 
{
    public static void Main()
    {
        IEnumerable<Match> matches = 
	    MatchReader.ReadHandsIntoMatches("Poker/poker.txt");
	IEnumerable<bool> didPlayerOneWin =
            matches.Select(match =>
		MatchEvaluator.DoesPlayerOneWin(match));
        Console.WriteLine(didPlayerOneWin.Sum(b => b ? 1 : 0));
    }
}
