﻿using GameManagement;
using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public abstract class Attraction : MonoBehaviour
    {
        public ViewRadius viewRadius;
        public MoneyBag moneyBag;
            
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
    }
}