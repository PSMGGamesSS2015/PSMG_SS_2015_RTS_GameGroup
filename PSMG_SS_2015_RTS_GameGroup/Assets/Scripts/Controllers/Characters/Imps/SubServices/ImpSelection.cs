using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpSelection : MonoBehaviour
    {
        private SpriteRenderer[] components;

        public void Awake()
        {
            components = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Start()
        {
            Hide();
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