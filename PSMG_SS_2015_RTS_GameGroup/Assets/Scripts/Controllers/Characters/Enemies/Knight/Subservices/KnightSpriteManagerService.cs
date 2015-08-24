using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightSpriteManagerService : MonoBehaviour
    {
        private SpriteRenderer knightTart;

        public void Awake()
        {
            knightTart =
                GetComponentsInChildren<SpriteRenderer>().First(sr => sr.gameObject.tag == TagReferences.KnightTart);

            HideTart();
        }

        public void DisplayTart()
        {
            knightTart.enabled = true;
        }

        public void HideTart()
        {
            knightTart.enabled = false;
        }
    }
}