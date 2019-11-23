using System;
using System.Collections.Generic;

namespace Poker
{
    public class Hand
    {
        public IEnumerable<Card> Cards { get; }

	public Hand(IEnumerable<Card> cards)
	{
            Cards = cards;
	}
    }
}
