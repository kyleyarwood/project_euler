using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Poker
{
    public static class MatchReader
    {
        public static IEnumerable<Match> ReadHandsIntoMatches(string fileName)
	{
	    IEnumerable<string> fileLines = 
		File.ReadAllLines(fileName, Encoding.UTF8);
            
            return fileLines.Select(fileLine => ReadHandsIntoMatch(fileLine));
	}

        private static Match ReadHandsIntoMatch(string fileLine)
	{
            IEnumerable<string> cardStrings = fileLine.Split(' ');

	    return new Match(
		ReadCardsIntoHand(cardStrings.Take(5)),
		ReadCardsIntoHand(cardStrings.Skip(5)));
	}

	private static Hand ReadCardsIntoHand(IEnumerable<string> cardStrings)
	{
           return new Hand(
		cardStrings.Select(
		    cardString => 
			ConstructCard(cardString)));
	}

	private static Card ConstructCard(string cardString)
	{
            return new Card(GetValue(cardString[0]), GetSuit(cardString[1]));
	}

	private static Value GetValue(char valueChar)
	{
            switch (valueChar)
	    {
                case '2':
		    return Value.Two;
		case '3':
		    return Value.Three;
		case '4':
		    return Value.Four;
		case '5':
		    return Value.Five;
		case '6':
		    return Value.Six;
		case '7':
		    return Value.Seven;
		case '8':
		    return Value.Eight;
		case '9':
		    return Value.Nine;
		case 'T':
		    return Value.Ten;
		case 'J':
		    return Value.Jack;
		case 'Q':
		    return Value.Queen;
		case 'K':
		    return Value.King;
		case 'A':
		    return Value.Ace;
		default:
		    throw new ArgumentException(valueChar.ToString());
	    }
	}

	private static Suit GetSuit(char suitChar)
	{
            switch (suitChar)
	    {
                case 'H':
		    return Suit.Heart;
		case 'C':
		    return Suit.Club;
		case 'S':
		    return Suit.Spade;
		case 'D':
		    return Suit.Diamond;
		default:
		    throw new ArgumentException(suitChar.ToString());
	    }
	}
    }
}
