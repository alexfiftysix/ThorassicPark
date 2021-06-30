using GameManagement;
using Phase_1.Builder.Buildings.ArrowPen;
using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public abstract class Attraction : MonoBehaviour
    {
        public ViewRadius viewRadius;
        public MoneyBag moneyBag;
        public float breakChancePercent;
        public OverlapCheck overlapCheck;
        
        // Ghost building
        public SpriteRenderer spriteRenderer;
        public Material ghostMaterial;
        private Material _standardMaterial;
        private static readonly int GhostShaderColor = Shader.PropertyToID("Color_c9794d5cc0484bfb99bcbf82f83078e6");

        protected virtual void Awake()
        {
            _standardMaterial = spriteRenderer.material;
        } 

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
            UnGhostify();
        }

        public virtual void Break()
        {
            
        }

        public virtual bool CanBePlaced()
        {
            return false;
        }

        public virtual void Ghostify()
        {
            spriteRenderer.material = ghostMaterial;
        }

        private void UnGhostify()
        {
            spriteRenderer.material = _standardMaterial;
        }

        public virtual void SetColor(Color newColor)
        {
            spriteRenderer.material.SetColor(GhostShaderColor, newColor);
        }
    }
}