using System;
using System.Linq;
using System.Collections.Generic;

namespace Poker
{
    public static class MatchEvaluator
    {
	private static readonly IEnumerable<Value> cardValues = 
	    Enum.GetValues(typeof(Value)).Cast<Value>();
	private static readonly IEnumerable<Suit> cardSuits =
	    Enum.GetValues(typeof(Value)).Cast<Suit>();

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
            
	    return TieBreaker(
		playerOneHandRating,
		match.PlayerOneHand,
		match.PlayerTwoHand);
        }

	public static HandRating GetHandRating(Hand hand)
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
		hand.Cards.Min(card => card.CardValue) == Value.Ten;
	}

	private static bool HandIsStraightFlush(Hand hand)
	{
            return HandIsStraight(hand) && HandIsFlush(hand);
	}

	private static bool HandIsFourOfAKind(Hand hand)
	{
            return cardValues.Any(cardValue =>
		hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 4);
	}

	private static bool HandIsFullHouse(Hand hand)
	{
            return HandIsThreeOfAKind(hand) && HandIsTwoPair(hand);
	}

	private static bool HandIsFlush(Hand hand)
	{
            return cardSuits.Any(
	        suit => hand.Cards.All(card => card.CardSuit == suit));
	}

	private static bool HandIsStraight(Hand hand)
	{
            IEnumerable<int> orderedCardValues =
		hand.Cards.Select(card => (int)(card.CardValue))
		    .OrderBy(cardValue => cardValue);

	    return Enumerable.Range(1, 4).All(
	        i => orderedCardValues.ElementAt(i) ==
		    orderedCardValues.ElementAt(i - 1) + 1) ||
		Enumerable.Range(1, 3).All(
		i => orderedCardValues.ElementAt(i) ==
		    orderedCardValues.ElementAt(i - 1) + 1) &&
		orderedCardValues.First() == (int)Value.Two &&
		orderedCardValues.Last() == (int)Value.Ace;
	}

	private static bool HandIsThreeOfAKind(Hand hand)
	{
            return cardValues.Any(cardValue =>
	        hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 3);
	}

	private static bool HandIsTwoPair(Hand hand)
	{
            return cardValues.Where(cardValue =>
		hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 2).Count() == 2;
	}

	private static bool HandIsOnePair(Hand hand)
	{
            return cardValues.Any(cardValue =>
	        hand.Cards.Where(card => card.CardValue == cardValue)
		    .Count() >= 2);
	}

	private static bool TieBreaker(
	    HandRating handRating,
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            switch (handRating)
	    {
                case HandRating.RoyalFlush:
		    return true;
		case HandRating.StraightFlush:
		    return TieBreakStraightFlush(playerOneHand, playerTwoHand);
		case HandRating.FourOfAKind:
		    return TieBreakFourOfAKind(playerOneHand, playerTwoHand);
		case HandRating.FullHouse:
		    return TieBreakFullHouse(playerOneHand, playerTwoHand);
		case HandRating.Flush:
		    return TieBreakFlush(playerOneHand, playerTwoHand);
		case HandRating.Straight:
		    return TieBreakStraight(playerOneHand, playerTwoHand);
		case HandRating.ThreeOfAKind:
		    return TieBreakThreeOfAKind(playerOneHand, playerTwoHand);
		case HandRating.TwoPair:
		    return TieBreakTwoPair(playerOneHand, playerTwoHand);
		case HandRating.OnePair:
		    return TieBreakOnePair(playerOneHand, playerTwoHand);
		default:
		    return TieBreakHighCard(playerOneHand, playerTwoHand);
	    }
	}

	private static bool TieBreakStraightFlush(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            return TieBreakStraight(playerOneHand, playerTwoHand);
	}

        private static bool TieBreakFourOfAKind(
	    Hand playerOneHand, 
	    Hand playerTwoHand)
	{
            Value playerOneValue = GetNOfAKindValue(playerOneHand, 4);
	    Value playerTwoValue = GetNOfAKindValue(playerTwoHand, 4);
	    
	    if (playerOneValue > playerTwoValue)
	    {
                return true;
	    }
	    else if (playerTwoValue > playerOneValue)
	    {
		return false;
	    }

	    return playerTwoHand.Cards.Single(card => 
		card.CardValue != playerTwoValue).CardValue <=
		playerOneHand.Cards.Single(card =>
	        card.CardValue != playerOneValue).CardValue;
	}

	private static bool TieBreakFullHouse(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            Value playerOneThreeValue = GetNOfAKindValue(playerOneHand, 3);
	    Value playerOneTwoValue = GetNOfAKindValue(playerOneHand, 2);
	    Value playerTwoThreeValue = GetNOfAKindValue(playerTwoHand, 3);
	    Value playerTwoTwoValue = GetNOfAKindValue(playerTwoHand, 2);

	    if (playerOneThreeValue > playerTwoThreeValue)
	    {
                return true;
	    }
	    else if (playerTwoThreeValue > playerOneThreeValue)
	    {
	        return false;
	    }
	    else
	    {
                return playerTwoTwoValue <= playerOneTwoValue;
	    }
	}

	private static bool TieBreakFlush(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            return TieBreakHighCard(playerOneHand, playerTwoHand);
	}

	private static bool TieBreakStraight(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            Value playerOneHighEndValue = 
		playerOneHand.Cards.Max(card => card.CardValue);
	    Value playerTwoHighEndValue =
		playerTwoHand.Cards.Max(card => card.CardValue);

	    return playerOneHighEndValue >= playerTwoHighEndValue &&
		!(playerOneHighEndValue == Value.Ace &&
	            playerOneHand.Cards.Min(card => card.CardValue) == Value.Two) ||
		playerTwoHighEndValue == Value.Ace &&
		    playerTwoHand.Cards.Min(card => card.CardValue) == Value.Two;
	}

	private static bool TieBreakThreeOfAKind(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            Value playerOneThreeValue = GetNOfAKindValue(playerOneHand, 3);
	    Value playerTwoThreeValue = GetNOfAKindValue(playerTwoHand, 3);

	    if (playerOneThreeValue > playerTwoThreeValue)
	    {
		return true;
	    }
	    else if (playerTwoThreeValue > playerOneThreeValue)
	    {
		return false;
	    }

	    IEnumerable<Card> playerOneLeftCards = playerOneHand.Cards
		.Where(card => card.CardValue != playerOneThreeValue)
		    .OrderByDescending(card => card.CardValue);

	    IEnumerable<Card> playerTwoLeftCards = playerTwoHand.Cards
		.Where(card => card.CardValue != playerTwoThreeValue)
		    .OrderByDescending(card => card.CardValue);

	    for (int i = 0; i < playerOneLeftCards.Count(); ++i)
	    {
                if (playerOneLeftCards.ElementAt(i).CardValue > 
		    playerTwoLeftCards.ElementAt(i).CardValue)
		{
                    return true;
		}
		else if (playerTwoLeftCards.ElementAt(i).CardValue >
		    playerOneLeftCards.ElementAt(i).CardValue)
		{
                    return false;
		}
	    }

	    return true;
	}

	private static bool TieBreakTwoPair(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            return TieBreakOnePair(playerOneHand, playerTwoHand);
	}

	private static bool TieBreakOnePair(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            List<Value> playerOnePairedValues = GetPairedValues(playerOneHand);
	    List<Value> playerTwoPairedValues = GetPairedValues(playerTwoHand);

	    for (int i = 0; i < playerOnePairedValues.Count; ++i)
	    {
                if (playerOnePairedValues.ElementAt(i) >
		    playerTwoPairedValues.ElementAt(i))
		{
                    return true;
		}
		else if (playerTwoPairedValues.ElementAt(i) >
		    playerOnePairedValues.ElementAt(i))
		{
                    return false;
		}
	    }

	    List<Value> playerOneNonPaired = 
		GetNonPaired(playerOneHand, playerOnePairedValues);

	    List<Value> playerTwoNonPaired =
		GetNonPaired(playerTwoHand, playerTwoPairedValues);

	    for (int i = 0; i < playerOneNonPaired.Count; ++i)
	    {
                if (playerOneNonPaired.ElementAt(i) >
		    playerTwoNonPaired.ElementAt(i))
		{
                    return true;
		}
		else if (playerTwoNonPaired.ElementAt(i) >
		    playerOneNonPaired.ElementAt(i))
		{
                    return false;
		}
	    }

	    return true;
	}

	private static List<Value> GetNonPaired(Hand hand, List<Value> paired)
	{
            return hand.Cards.Select(card => card.CardValue)
		.Where(cardValue => !paired.Contains(cardValue))
		.OrderByDescending(cardValue => cardValue).ToList();
	}

	private static bool TieBreakHighCard(
	    Hand playerOneHand,
	    Hand playerTwoHand)
	{
            List<Value> playerOneOrderedCards = playerOneHand.Cards
	        .Select(card => card.CardValue)
		.OrderByDescending(cardValue => cardValue).ToList();
	    List<Value> playerTwoOrderedCards = playerTwoHand.Cards
	        .Select(card => card.CardValue)
		.OrderByDescending(cardValue => cardValue).ToList();

	    for (int i = 0; i < playerOneOrderedCards.Count; ++i)
	    {
                if (playerOneOrderedCards.ElementAt(i) >
	            playerTwoOrderedCards.ElementAt(i))
		{
                    return true;
		}
		else if (playerTwoOrderedCards.ElementAt(i) >
		    playerOneOrderedCards.ElementAt(i))
		{
	            return false;
		}
	    }

	    return true;
	}

	private static List<Value> GetPairedValues(Hand hand)
	{
            return hand.Cards.Select(card => card.CardValue)
		.Where(cardValue => hand.Cards.Where(card =>
		    card.CardValue == cardValue).Count() == 2)
		.OrderByDescending(cardValue => cardValue).ToList();
	}

	private static Value GetNOfAKindValue(Hand hand, int n)
	{
	    return cardValues.Single(cardValue => 
	        hand.Cards.Where(card =>
		    card.CardValue == cardValue).Count() == n);
	}
    }
}
