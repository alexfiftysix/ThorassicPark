using System;
using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder.Buildings;
using Phase_1.Builder.DeckBuilder;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.Extensions;

namespace Phase_1.Builder
{
    public class Hand : MonoBehaviour
    {
        public List<AttractionCard> cards;
        private Deck _deck;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _deck = FindObjectOfType<Deck>();
        }
        

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Game" && IsEmpty()) // TODO: String bad!
            {
                FillHandWithRandomCards();
            }
        }

        private void FillHandWithRandomCards()
        {
            var choices = _deck.attractions.Where(a => a.isUnlocked).Shuffled().ToArray();
            
            for (var i = 0; i < cards.Count; i++)
            {
                cards[i].SetAttraction(choices[i].attraction);
            }
        }

        public void SetCard(AttractionCard card, int index)
        {
            RequireIndexInRange(index);

            var attraction = cards[index].attraction;
            cards[index] = card;
            card.SetAttraction(attraction);
        }

        /// <summary>
        /// Sets an attraction card in the hand
        /// </summary>
        /// <param name="index">index to set attraction</param>
        /// <param name="attraction">card to set</param>
        /// <returns>True if attraction is successfully set</returns>
        public bool Add(Attraction attraction, int index = -1)
        {
            if (IsFull()) return false;

            if (index == -1)
            {
                if (IsFull())
                {
                    return false;
                }

                cards.First(c => c.isEmpty).SetAttraction(attraction);
                return true;
            }

            RequireIndexInRange(index);

            cards[index].SetAttraction(attraction);
            return true;
        }

        public bool AlreadyInHand(Attraction attraction)
        {
            return cards.Exists(c => !(c is null) && !c.isEmpty && c.attraction.name == attraction.name); // TODO: Checking on name is bad
        }

        public void Remove(Attraction attraction)
        {
            if (AlreadyInHand(attraction))
            {
                cards.First(c => !c.isEmpty && c.attraction.name == attraction.name)
                    .SetAttraction(null); // TODO: Checking on name is bad
            }
        }

        private bool IsFull()
        {
            return cards.All(c => !c.isEmpty);
        }

        private bool IsEmpty()
        {
            return cards.All(c => c.isEmpty);
        }

        public Attraction GetAttraction(int index)
        {
            RequireIndexInRange(index);

            return cards[index].attraction;
        }

        private void RequireIndexInRange(int index)
        {
            if (index >= cards.Count)
            {
                throw new IndexOutOfRangeException(
                    $"You've got [{cards.Count}] cards in your hand, but asked for card at index {index}");
            }
        }
    }
}