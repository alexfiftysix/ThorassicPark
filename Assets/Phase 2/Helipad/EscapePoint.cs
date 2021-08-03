using System;
using Configuration;
using GameManagement;
using UnityEngine;

namespace Phase_2.Helipad
{
    public class EscapePoint : MonoBehaviour
    {
        public GameManager manager;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer(Configuration.Configuration.Layers[Layer.Player]))
            {
                manager.PlayerEscaped();
            }
        }
    }
}
