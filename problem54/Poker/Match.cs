using System;

namespace Poker
{
    public class Match
    {
        public Hand PlayerOneHand { get; }
	public Hand PlayerTwoHand { get; }

	public Match(Hand playerOneHand, Hand playerTwoHand)
	{
            PlayerOneHand = playerOneHand;
	    PlayerTwoHand = playerTwoHand;
	}
    }
}
