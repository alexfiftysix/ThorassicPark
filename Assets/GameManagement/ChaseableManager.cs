using System.Collections.Generic;
using UnityEngine;
using Utilities.Extensions;
using Visitors;

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
