using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utilities.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buildings.DeckBuilder
{
    public class Hand : MonoBehaviour
    {
        public List<AttractionCard> cards;
        private List<Unlockable> _attractions; // TODO: Don't like having 2 lists conveying very similar information
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
            _attractions = new List<Unlockable>(_maxLength);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == Configuration.Configuration.Scenes[Configuration.Scene.Game])
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

            var choices = _deck.unlockables.Where(a => a.isUnlocked).Shuffled().ToArray();

            for (var i = 0; i < cards.Count; i++)
            {
                Add(choices[i]);
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
        /// <param name="unlockable"></param>
        /// <returns>True if added, false if removed</returns>
        public bool Toggle(Unlockable unlockable)
        {
            if (Contains(unlockable.attraction))
            {
                Remove(unlockable);
                return false;
            }

            Add(unlockable);
            return true;
        }

        public bool Contains(Attraction attraction)
        {
            return _attractions.Exists(a => a.name == attraction.name);
        }

        private void Remove(Unlockable unlockable)
        {
            if (!Contains(unlockable.attraction)) return;

            // TODO: Checking on name is bad
            cards.First(c => c.unlockable.name == unlockable.name).SetAttraction(null);
            _attractions = _attractions.Where(a => a.name != unlockable.name).ToList();
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

            return _attractions[index].attraction;
        }

        private void RequireIndexInRange(int index)
        {
            if (index >= cards.Count)
            {
                throw new IndexOutOfRangeException(
                    $"Your hand has capacity [{_maxLength}], but asked for card at index {index}");
            }
        }

        private void Add(Unlockable attraction, int index = -1)
        {
            if (IsFull()) return;
            if (!attraction.isUnlocked) return;

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