using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        public virtual int GetCost()
        {
            return 0;
        }
        
        public virtual bool IsBroken()
        {
            return false;
        }
        
        public virtual void Build() {}
    }
}