using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    /// <summary>
    /// The imp inventory contains the sprite renderers of the tools
    /// used by an imp. 
    /// </summary>

    public class ImpInventory : MonoBehaviour
    {
        private SpriteRenderer spear;
        private SpriteRenderer shield;
        private SpriteRenderer bomb;
        private SpriteRenderer ladder;
        private Explosion explosion;

        private List<SpriteRenderer> tools;

        #region initialization

        public void Awake()
        {
            tools = new List<SpriteRenderer>();
        }

        public void Start()
        {
            RetrieveTools();
            HideAllTools();
        }

        private void RetrieveTools()
        {
            var renderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (var renderer in renderers)
            {
                if (renderer.gameObject.tag == TagReferences.ImpInventorySpear)
                {
                    spear = renderer;
                }
                if (renderer.gameObject.tag == TagReferences.ImpInventoryShield)
                {
                    shield = renderer;
                }
                if (renderer.gameObject.tag == TagReferences.ImpInventoryBomb)
                {
                    bomb = renderer;
                }
                if (renderer.gameObject.tag == TagReferences.ImpInventoryLadder)
                {
                    ladder = renderer;
                }
                tools.Add(renderer);
            }

            explosion = GetComponentInChildren<Explosion>();
        }
    
        public void HideAllTools()
        {
            foreach (var renderer in tools)
            {
                renderer.enabled = false;
            }
        }

        #endregion

        public Explosion Explo
        {
            get
            {
                return explosion;
            }
        }

        public void Display(string item)
        {
            HideAllTools();
            switch (item)
            {
                case "Spear":
                    DisplaySpear();
                    break;
                case "Shield":
                    DisplayShield();
                    break;
                case "Ladder":
                    DisplayLadder();
                    break;
                case "Bomb":
                    DisplayBomb();
                    break;
                case "Explosion":
                    explosion.Display();
                    break;
            }
        
        }

        public void DisplaySpear()
        {
            spear.enabled = true;
        }


        public void DisplayLadder()
        {
            ladder.enabled = true;
        }

        public void DisplayBomb()
        {
            bomb.enabled = true;
        }

        public void DisplayShield()
        {
            shield.enabled = true;
        }

        public void DisplayExplosion()
        {
            explosion.Display();
        }
    }
}