using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class Explosion : MonoBehaviour
    {

        private SpriteRenderer[] components;

        public void Awake()
        {
            components = GetComponentsInChildren<SpriteRenderer>();
        }


        public void Display()
        {
            foreach (var r in components)
            {
                r.enabled = true;
            }
        }

        public void Hide()
        {
            foreach (var r in components)
            {
                r.enabled = false;
            } 
        }
    }
}