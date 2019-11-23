using System;

namespace Poker
{
    public class Card
    {
        public Value CardValue { get; }
	public Suit CardSuit { get; }

	public Card(Value cardValue, Suit cardSuit)
	{
            CardValue = cardValue;
	    CardSuit = cardSuit;
	}
    }
}
