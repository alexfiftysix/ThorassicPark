using System.Collections.Generic;
using Characters.Visitors;
using Common.Utilities.Extensions;
using UnityEngine;

namespace GameManagement
{
    public class ChaseableManager : MonoBehaviour
    {
        private List<IChaseable> _chaseables;

        public void Awake()
        {
            _chaseables = new List<IChaseable>();
        }

        public void Add(IChaseable chaseable)
        {
            _chaseables.Add(chaseable);
        }

        public void Remove(IChaseable chaseable)
        {
            _chaseables.Remove(chaseable);
        }

        public IChaseable GetRandom()
        {
            return _chaseables.RandomChoice();
        }
    }
}
