using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Models.Combat;

public enum MODIFIER_DECK_NEW_CARD_POSITION
{
    top,
    bottom,
    center,
    random,
    afterSpecific,
    beforeSpecific
}

public class AttackModifierDeck
{
    public Guid Id { get; }
    public Dictionary<int, AttackModifier> Deck { get; private set;}

    public List<int> Positions { get; private set; } = new List<int>(); // Current Flipped Card
    public List<AttackModifier> ShownCards { get {
        if(Positions.Count() == 0) return new List<AttackModifier>();
        return Deck
            .Where(kvp => kvp.Key >= Positions.Min() && kvp.Key <= Positions.Max())
            .Select(kvp => kvp.Value)
            .ToList();
    }}
    public Dictionary<int, AttackModifier> DiscardPile { get {
        if(Positions.Count() == 0) return new Dictionary<int, AttackModifier>();
        return Deck
            .Where(kvp => kvp.Key < Positions.Min())
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }}
    public Dictionary<int, AttackModifier> DrawPile { get{
        if(Positions.Count() == 0) return Deck;
        return Deck
            .Where(kvp => kvp.Key > Positions.Max())
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }}

    public AttackModifierDeck(Guid id, Dictionary<int, AttackModifier> deck, List<int> positions)
    {
        Id = id;
        Deck = deck;
        Positions = positions;
    }

    public AttackModifierDeck(Guid id, Dictionary<int, AttackModifier> deck)
    {
        Id = id;
        Deck = deck;
        Positions = new List<int>();
        ShuffleDeck();
    }

    public AttackModifierDeck(List<AttackModifier> deck)
    {
        Id = Guid.NewGuid();
        Deck = AddCardsToModifierDeck(new Dictionary<int, AttackModifier>(), deck, 0, MODIFIER_DECK_NEW_CARD_POSITION.random);
    }

    public List<AttackModifier> DrawCards (int numberOfCards)
    {
        int lastMax = Positions.Max();
        Positions.Clear();
        for (int i = 1; i < numberOfCards; i++)
        {
            int maxPos = i + lastMax;
            if(!Deck.Keys.Contains(maxPos))
            {
                lastMax = 0;
                maxPos = i;
                ShuffleDiscardPile();
            }
            Positions.Add(maxPos);
        }

        return ShownCards;
    }

    public List<AttackModifier> RedrawCards (int numberOfCards)
    {
        if(Positions.Count() > 0 )
        {
            int min = Positions.Min();
            if(min == 1) Positions.Clear();
            else 
            {
                Positions.Clear();
                Positions.Add(min - 1);
            }
        }
        
        ShuffleDrawPile();

        return DrawCards(numberOfCards);
    }

    
    public void AddNewCardsToRandomPositionsOfDeck(List<AttackModifier> newCards)
    {
        Deck = AddCardsToModifierDeck(Deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.random);
    }

    public void AddNewCardsToTopOfDeck(List<AttackModifier> newCards)
    {
        Deck = AddCardsToModifierDeck(Deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.top);
    }

    public void AddNewCardsToBottomOfDeck(List<AttackModifier> newCards)
    {
        Deck = AddCardsToModifierDeck(Deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.bottom);
    }

    public void AddNewCardsToCenterOfDeck(List<AttackModifier> newCards)
    {
        Deck = AddCardsToModifierDeck(Deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.center);
    }

    public void ShuffleDeck()
    {
        List<AttackModifier> cardsToShuffle = Deck.Select(kvp => kvp.Value).ToList();
        int maxDeckPosition = 0;
        Deck = AddCardsToModifierDeck(new Dictionary<int, AttackModifier>(), cardsToShuffle, maxDeckPosition, MODIFIER_DECK_NEW_CARD_POSITION.random);
        Positions.Clear();
    }

    private void ShuffleDiscardPile()
    {          
        List<AttackModifier> cardsToShuffle = DiscardPile.Select(kvp => kvp.Value).ToList();
        int maxDeckPosition = Positions.Max();
        Deck = AddCardsToModifierDeck(new Dictionary<int, AttackModifier>(), cardsToShuffle, maxDeckPosition, MODIFIER_DECK_NEW_CARD_POSITION.random);
    }

    private void ShuffleDrawPile()
    {
        Dictionary<int, AttackModifier> baseDeck = DiscardPile;
        List<AttackModifier> cardsToShuffle = DrawPile.Select(kvp => kvp.Value).ToList();
        int maxDeckPosition = Positions.Max();
        Deck = AddCardsToModifierDeck(baseDeck, cardsToShuffle, maxDeckPosition, MODIFIER_DECK_NEW_CARD_POSITION.random);
    }

    private Dictionary<int, AttackModifier> AddCardsToModifierDeck(Dictionary<int, AttackModifier> currentDeck, List<AttackModifier> newCards, int indexFrom, MODIFIER_DECK_NEW_CARD_POSITION position, int specificPos = 0)
    {
        if (newCards.Count > 9999) throw new ArgumentException("Cannot add more than 9999 modifier cards to a deck at a time.");
        if ((position == MODIFIER_DECK_NEW_CARD_POSITION.afterSpecific || position == MODIFIER_DECK_NEW_CARD_POSITION.beforeSpecific) && specificPos == 0) throw new ArgumentException("If beforeSpecific or afterSpecific selected, non 0 specific position must be provided");

        Dictionary<int, AttackModifier> updatedDeck = currentDeck.ToDictionary(kvp => kvp.Key * 10000, kvp => kvp.Value);
        int cardPos = 0;

        if (currentDeck.Count > 0)
        {
            if (position == MODIFIER_DECK_NEW_CARD_POSITION.bottom)
            {
                // Add to end of deck (highest position number)
                cardPos = updatedDeck.Keys.Max();
            }
            else if (position == MODIFIER_DECK_NEW_CARD_POSITION.center)
            {
                // add after middle card in deck (middle position number)
                cardPos = updatedDeck.Keys.AsEnumerable().GetMedian();
            }
            else if (position == MODIFIER_DECK_NEW_CARD_POSITION.top)
            {
                cardPos = 0;
            }
            else if (position == MODIFIER_DECK_NEW_CARD_POSITION.afterSpecific || position == MODIFIER_DECK_NEW_CARD_POSITION.beforeSpecific)
            {
                cardPos = specificPos;
            }
        }
        else if (position == MODIFIER_DECK_NEW_CARD_POSITION.beforeSpecific)
        {
            cardPos = 10000;
        }

        if (position == MODIFIER_DECK_NEW_CARD_POSITION.random)
        {
            var maxValue = updatedDeck.Count > 0 ? updatedDeck.Keys.OrderBy(k => k).Max() + 10000 : 1000000;
            var rand = new Random();
            newCards.ForEach(card =>
            {
                var success = false;
                do
                {
                    success = updatedDeck.TryAdd(rand.Next(maxValue), card);
                } while (!success);
            });
        }
        else
        {
            newCards.ForEach(card =>
            {
                if (position != MODIFIER_DECK_NEW_CARD_POSITION.beforeSpecific)
                {
                    cardPos++;
                }
                else
                {
                    cardPos--;
                }
                updatedDeck.TryAdd(cardPos, card);
            });
        }

        Dictionary<int, AttackModifier> deckForReturn = new Dictionary<int, AttackModifier>();
        cardPos = indexFrom;
        updatedDeck.OrderBy(kvp => kvp.Key).ToList().ForEach(kvp =>
        {
            deckForReturn.TryAdd(cardPos, kvp.Value);
            cardPos++;
        });

        return deckForReturn;

    }
}


public static class Helpers
{
    public static int GetMedian(this IEnumerable<int> source)
    {
        // Create a copy of the input, and sort the copy
        int[] temp = source.ToArray();
        Array.Sort(temp);
        int count = temp.Length;
        if (count == 0)
        {
            throw new InvalidOperationException("Empty collection");
        }
        else if (count % 2 == 0)
        {
            // count is even, average two middle elements
            return temp[(count - 1) / 2];
        }
        else
        {
            // count is odd, return the middle element
            return temp[count / 2];
        }
    }
}

public class AttackModifierDeckDTO
{
    public int DrawPileCount { get; set; }
    public int DiscardPileCount { get; set;}
    public List<AttackModifier> ShownCards { get; set; } = new List<AttackModifier>();
}