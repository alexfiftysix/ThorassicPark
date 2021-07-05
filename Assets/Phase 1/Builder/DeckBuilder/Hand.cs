using System;
using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder.Buildings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.Extensions;

namespace Phase_1.Builder.DeckBuilder
{
    public class Hand : MonoBehaviour
    {
        public List<AttractionCard> cards; // TODO: Why are these going null on restart??
        private List<Attraction> _attractions; // TODO: Don't like having 2 lists conveying very similar information
        private int _maxLength;
        private Deck _deck;
        private static Hand _instance;

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            FindDeck();
            _maxLength = cards.Count;
            _attractions = new List<Attraction>(_maxLength);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Game") // TODO: String bad!
            {
                FillHandWithRandomAttractions();
            }
        }

        private void FindDeck()
        {
            if (_deck is null)
            {
                _deck = FindObjectOfType<Deck>();
            }
        }

        private void FillHandWithRandomAttractions()
        {
            if (!IsEmpty()) return;

            // FindDeck();

            var choices = _deck.attractions.Where(a => a.isUnlocked).Shuffled().ToArray();

            for (var i = 0; i < cards.Count; i++)
            {
                Add(choices[i].attraction);
            }
        }

        public void SetCard(AttractionCard card, int index)
        {
            RequireIndexInRange(index);

            cards[index] = card;
            card.SetAttraction(_attractions[index]);
        }

        
        /// <summary>
        /// Add or remove attraction from Hand
        /// </summary>
        /// <param name="attraction"></param>
        /// <returns>True if added, false if removed</returns>
        public bool Toggle(Attraction attraction)
        {
            if (Contains(attraction))
            {
                Remove(attraction);
                return false;
            }

            Add(attraction);
            return true;
        }
        
        public bool Contains(Attraction attraction)
        {
            return cards.Exists(c =>
                !(c is null) && !c.isEmpty && c.attraction.name == attraction.name); // TODO: Checking on name is bad
        }

        private void Remove(Attraction attraction)
        {
            if (Contains(attraction))
            {
                // TODO: Checking on name is bad
                cards.First(c => c.attraction.name == attraction.name).SetAttraction(null);
                _attractions = _attractions.Where(a => a.name != attraction.name).ToList();
            }
        }

        private bool IsFull()
        {
            return _attractions.Count == _maxLength;
        }

        private bool IsEmpty()
        {
            return _attractions.Count == 0;
        }

        public Attraction GetAttraction(int index)
        {
            RequireIndexInRange(index);

            return _attractions[index];
        }

        private void RequireIndexInRange(int index)
        {
            if (index >= cards.Count)
            {
                throw new IndexOutOfRangeException($"Your hand has capacity [{_maxLength}], but asked for card at index {index}");
            }
        }
        
        private void Add(Attraction attraction, int index = -1)
        {
            if (IsFull()) return;

            if (index == -1)
            {
                if (IsFull())
                {
                    return;
                }

                cards.First(c => c.isEmpty).SetAttraction(attraction); // TODO: Set attraction the same way?
                _attractions.Add(attraction);
                return;
            }

            RequireIndexInRange(index);

            cards[index].SetAttraction(attraction);
            _attractions[index] = attraction;
        }

    }
}