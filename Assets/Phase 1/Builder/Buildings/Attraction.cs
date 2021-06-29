using GameManagement;
using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public abstract class Attraction : MonoBehaviour
    {
        public ViewRadius viewRadius;
        public MoneyBag moneyBag;
        public float breakChancePercent = 0;

        public virtual int GetCost()
        {
            return 0;
        }
        
        public virtual bool IsBroken()
        {
            return false;
        }

        public virtual void Build(MoneyBag newMoneyBag)
        {
            moneyBag = newMoneyBag;
        }

        public bool IsViewRadius(GameObject other)
        {
            return other.GetComponent<ViewRadius>() == viewRadius;
        }

        public virtual void Break()
        {
            
        }

        public virtual bool CanBePlaced()
        {
            return false;
        }

        public virtual void SetColor(Color newColor)
        {
            Debug.Log("Default Set Color");
        }
    }
}