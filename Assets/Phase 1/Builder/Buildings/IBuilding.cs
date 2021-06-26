using UnityEngine;

namespace Visitors
{
    public interface IBuilding
    {
        // Returns cost of building
        public int Build(Vector3 position);

        public int Cost { get; }

        public IBuilding GetGhostBuilding(Vector3 position);

        public void MoveTo(Vector3 position);
        
        public void Delete();
    }
}
