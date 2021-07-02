using System.Collections.Generic;
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
        public bool isGhost;

        // Break
        public List<GameObject> walls;
        public bool isBroken = false;

        // Monsters
        public GameObject monster;
        public int monsterCount = 3;

        public int cost = 1;

        protected virtual void Awake()
        {
            _standardMaterial = spriteRenderer.material;
        }

        public virtual void Build(MoneyBag newMoneyBag)
        {
            moneyBag = newMoneyBag;
            UnGhostify();
            for (var i = 0; i < monsterCount; i++)
            {
                Instantiate(monster, transform.position - Vector3.forward, Quaternion.identity);
            }
        }

        public virtual void Break()
        {
            Destroy(viewRadius.gameObject);

            foreach (var wall in walls)
            {
                Destroy(wall);
            }
        }

        public virtual bool CanBePlaced()
        {
            return !overlapCheck.HasOverlap();
        }

        public virtual void Ghostify()
        {
            spriteRenderer.material = ghostMaterial;
            isGhost = true;
            SetWallColliders(false);
        }

        private void UnGhostify()
        {
            spriteRenderer.material = _standardMaterial;
            isGhost = false;
            SetWallColliders(true);
        }

        public virtual void SetColor(Color newColor)
        {
            spriteRenderer.material.SetColor(GhostShaderColor, newColor);
        }

        private void SetWallColliders(bool newState)
        {
            foreach (var wall in walls)
            {
                wall.GetComponent<Collider2D>().enabled = newState;
            }
        }
    }
}