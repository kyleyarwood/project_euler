using System;
using System.Linq;
using System.Collections.Generic;

namespace Poker
{
    public static class MatchEvaluator
    {
        public static bool DoesPlayerOneWin(Match match)
	{
            HandRating playerOneHandRating = 
		GetHandRating(match.PlayerOneHand);
	    HandRating playerTwoHandRating =
		GetHandRating(match.PlayerTwoHand);

	    if (playerOneHandRating > playerTwoHandRating)
	    {
                return true;
	    }
	    else if (playerOneHandRating < playerTwoHandRating)
	    {
                return false;
	    }
            
	    //TODO: tiebreakers
	    return true;
        }

	private static HandRating GetHandRating(Hand hand)
	{
	    //can't do switch case unless I give Hand a type, not sure
	    //what type a hand could really take on though
            if (HandIsRoyalFlush(hand))
	    {
	        return HandRating.RoyalFlush;
	    }
            else if (HandIsStraightFlush(hand))
	    {
		return HandRating.StraightFlush;
	    }
	    else if (HandIsFourOfAKind(hand))
	    {
	        return HandRating.FourOfAKind;
	    }
	    else if (HandIsFullHouse(hand))
	    {
		return HandRating.FullHouse;
	    }
	    else if (HandIsFlush(hand))
	    {
		return HandRating.Flush;
	    }
	    else if (HandIsStraight(hand))
	    {
		return HandRating.Straight;
	    }
	    else if (HandIsThreeOfAKind(hand))
	    {
		return HandRating.ThreeOfAKind;
	    }
	    else if (HandIsTwoPair(hand))
	    {
		return HandRating.TwoPair;
	    }
	    else if (HandIsOnePair(hand))
	    {
		return HandRating.OnePair;
	    }
	    else
	    {
		return HandRating.HighCard;
	    }
	}

	private static bool HandIsRoyalFlush(Hand hand)
	{
            return HandIsStraightFlush(hand) &&
		hand.Cards.Min(card => card.CardValue) == Value.Ten &&
		hand.Cards.Max(card => card.CardValue) == Value.Ace;
	}

	private static bool HandIsStraightFlush(Hand hand)
	{
            return HandIsStraight(hand) && HandIsFlush(hand);
	}

	private static bool HandIsFourOfAKind(Hand hand)
	{
            return GetValues().Any(cardValue =>
		hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 4);
	}

	private static bool HandIsFullHouse(Hand hand)
	{
            return HandIsThreeOfAKind(hand) && HandIsTwoPair(hand);
	}

	private static bool HandIsFlush(Hand hand)
	{
            return GetSuits().Any(
	        suit => hand.Cards.All(card => card.CardSuit == suit));
	}

	private static bool HandIsStraight(Hand hand)
	{
            IEnumerable<int> orderedCardValues =
		hand.Cards.Select(card => (int)(card.CardValue))
		    .OrderBy(cardValue => cardValue);

	    int minValue = orderedCardValues.Min();

            return orderedCardValues
		.Select(cardValue => cardValue - minValue) == 
		Enumerable.Range(0, 5) || 
		orderedCardValues.Take(4) == Enumerable.Range(2, 4) &&
		(Value)(orderedCardValues.Max()) == Value.Ace;
	}

	private static bool HandIsThreeOfAKind(Hand hand)
	{
            return GetValues().Any(cardValue =>
	        hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 3);
	}

	private static bool HandIsTwoPair(Hand hand)
	{
            return GetValues().Where(cardValue =>
		hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 2).Count() == 2;
	}

	private static bool HandIsOnePair(Hand hand)
	{
            return GetValues().Any(cardValue =>
	        hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 2);
	}

	private static IEnumerable<Suit> GetSuits()
	{
            return Enum.GetValues(typeof(Suit)).Cast<Suit>();
	}

	private static IEnumerable<Value> GetValues()
	{
	    return Enum.GetValues(typeof(Value)).Cast<Value>();
	}
    }
}
