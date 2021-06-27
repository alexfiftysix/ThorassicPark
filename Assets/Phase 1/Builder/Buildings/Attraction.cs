using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public abstract class Attraction : MonoBehaviour
    {
        public GameObject viewRadius; 
            
        public virtual int GetCost()
        {
            return 0;
        }
        
        public virtual bool IsBroken()
        {
            return false;
        }
        
        public virtual void Build() {}

        public bool IsViewRadius(GameObject gameObj)
        {
            return gameObj == viewRadius;
        }
    }
}