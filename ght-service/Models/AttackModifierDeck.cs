using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ATTACK_MODIFIER_TYPE
    {
        [EnumMember(Value = "add")]
        Add,

        [EnumMember(Value = "multiply")]
        Multiply,

        [EnumMember(Value = "cancel")]
        Cancel,
    }


    [Serializable]
    public class AttackModifier
    {
        [JsonPropertyName("type")]
        public ATTACK_MODIFIER_TYPE Type { get; set; }

        [JsonPropertyName("isCurse")]
        public bool IsCurse { get; set; } = false;

        [JsonPropertyName("isBlessing")]
        public bool IsBlessing { get; set; } = false;

        [JsonPropertyName("triggerShuffle")]
        public bool TriggerShuffle { get; set; } = false;

        [JsonPropertyName("value")]
        public int? Value { get; set; }
    }

    public enum MODIFIER_DECK_NEW_CARD_POSITION
    {
        top,
        bottom,
        center,
        random,
        afterSpecific,
        beforeSpecific
    }

    public class AttackModifierDeckDO
    {
        public List<AttackModifier> DrawPile {get; set;} = new List<AttackModifier>();
        public List<AttackModifier> DiscardPile {get; set;} = new List<AttackModifier>();
    }

    public class AttackModifierDeckDTO
    {
        public int DrawPileCount {get; set;}
        public int DiscardPileCount {get; set;}
    }

    public class AttackModifierDeck
    {
        private Dictionary<int, AttackModifier> _deck;
        private List<int> _discardPile = new List<int>();
        private List<int> _drawPile = new List<int>();
        private List<int> _flippedCards = new List<int>();

        public AttackModifierDeck(List<AttackModifier> cards)
        {
            _deck = new Dictionary<int, AttackModifier>();
            _deck = ShuffleCards(cards);
            _drawPile = _deck.Keys.AsEnumerable().Where(k => !_flippedCards.Contains(k)).OrderBy(k => k).ToList();
        }

        
        public AttackModifierDeck(AttackModifierDeckDO deck)
        {
            _deck = new Dictionary<int, AttackModifier>();

            int count = 1;
            deck.DiscardPile.ForEach(c => {
                _deck.Add(count, c);
                _discardPile.Add(count);
                count++;
            });

            deck.DrawPile.ForEach(c => {
                _deck.Add(count, c);
                _drawPile.Add(count);
                count++;
            });

            ShuffleDrawPile();
        }

        public List<AttackModifier> DrawNewCards(int count, Boolean redo = false)
        {
            if (count < 1) throw new ArgumentException("Have to flip at least 1 card.");
            if (count > _deck.Count) throw new ArgumentException("Cannot flip more cards than exist in deck.");

            if(redo)
            {
                _drawPile.AddRange(_flippedCards);
                _flippedCards.Clear();
                ShuffleDrawPile();
            }
            else
            {
                // Discard Current Flipped Cards.
                _discardPile.AddRange(_flippedCards);
                _flippedCards.Clear();
            }

            // Pull the next n cards from draw pile.
            for (int i = 1; i < count; i++)
            {
                if (_drawPile.Count < 1) ShuffleDeck();
                int nextCard = _drawPile.Where(k => !_discardPile.Contains(k) && !_flippedCards.Contains(k))
                    .OrderBy(k => k)
                    .FirstOrDefault(-1);
                if (nextCard != -1)
                {
                    _flippedCards.Add(nextCard);
                    _drawPile.Remove(nextCard);
                }
            }

            return GetFlippedCards();

        }

        public List<AttackModifier> GetFlippedCards()
        {
            return _flippedCards.Select(k => _deck.GetValueOrDefault(k, new AttackModifier())).ToList();
        }

        public void AddNewCardsToRandomPostionsOfDeck(List<AttackModifier> newCards)
        {
            _deck = addCardsToModifierDeck(_deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.random);
        }

        public void AddNewCardsToTopOfDeck(List<AttackModifier> newCards)
        {
            _deck = addCardsToModifierDeck(_deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.top);
        }

        public void AddNewCardsToBottomOfDeck(List<AttackModifier> newCards)
        {
            _deck = addCardsToModifierDeck(_deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.bottom);
        }

        public void AddNewCardsToCenterOfDeck(List<AttackModifier> newCards)
        {
            _deck = addCardsToModifierDeck(_deck, newCards, 0, MODIFIER_DECK_NEW_CARD_POSITION.center);
        }

        public void ShuffleDeck()
        {
            // Get a Shuffled Deck
            var cardsToShuffle = _deck.Values.ToList();
            var shuffledDeck = ShuffleCards(cardsToShuffle);

            // Clear out discard pile
            _discardPile.Clear();

            // add any cards that are not shown to the draw pile
            _drawPile = _deck.Keys.AsEnumerable().Where(k => !_flippedCards.Contains(k)).OrderBy(k => k).ToList();

            // persist shuffled deck
            _deck = shuffledDeck;
        }

        public void ShuffleDrawPile()
        {

            List<AttackModifier> cardsToShuffle = _drawPile.Select(c => _deck[c]).ToList();

            Dictionary<int, AttackModifier> drawnCards = _deck.Where(kvp => _discardPile.Contains(kvp.Key))
                .OrderBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            
            int indexFrom = drawnCards.Keys.Max() + 1;

            Dictionary<int, AttackModifier> shuffledDraw = ShuffleCards(cardsToShuffle, indexFrom);
            _drawPile = shuffledDraw.Keys.OrderBy(k => k).ToList();

            var updatedDeckKVPListing = drawnCards.ToList();
            updatedDeckKVPListing.AddRange(shuffledDraw.ToList());

            _deck = updatedDeckKVPListing.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        }

        private Dictionary<int, AttackModifier> ShuffleCards(List<AttackModifier> cards, int indexFrom = 0)
        {
            return addCardsToModifierDeck(new Dictionary<int, AttackModifier>(), cards, indexFrom, MODIFIER_DECK_NEW_CARD_POSITION.random);
        }


        private Dictionary<int, AttackModifier> addCardsToModifierDeck(Dictionary<int, AttackModifier> deck, List<AttackModifier> newCards, int indexFrom, MODIFIER_DECK_NEW_CARD_POSITION position, int specifcPos = 0)
        {
            if (newCards.Count > 9999) throw new ArgumentException("Cannot add more than 9999 modifier cards to a deck at a time.");
            if ((position == MODIFIER_DECK_NEW_CARD_POSITION.afterSpecific || position == MODIFIER_DECK_NEW_CARD_POSITION.beforeSpecific) && specifcPos == 0) throw new ArgumentException("If beforeSpecific or afterSpecific selected, non 0 specific postion must be provided");

            Dictionary<int, AttackModifier> updatedDeck = _deck.ToDictionary(kvp => kvp.Key * 10000, kvp => kvp.Value);
            int cardPos = 0;

            if (deck.Count > 0)
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
                    cardPos = specifcPos;
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
            updatedDeck.Keys.OrderBy(k => k).ToList().ForEach(k =>
            {
                var cardForAdd = updatedDeck.GetValueOrDefault(k);
                if (cardForAdd != null)
                {
                    deckForReturn.TryAdd(cardPos, cardForAdd);
                    cardPos++;
                }
            });

            return deckForReturn;

        }

        public AttackModifierDeckDO State => new AttackModifierDeckDO
            {
                DiscardPile = _deck.Where(kvp =>  _discardPile.Contains(kvp.Key) || _flippedCards.Contains(kvp.Key)).Select(kvp => kvp.Value).ToList(),
                DrawPile = _deck.Where(kvp => _drawPile.Contains(kvp.Key)).Select(kvp => kvp.Value).ToList()
            };

        public AttackModifierDeckDTO DTO => new AttackModifierDeckDTO
            {
                DiscardPileCount=_discardPile.Count + _flippedCards.Count,
                DrawPileCount=_drawPile.Count
            };
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



}