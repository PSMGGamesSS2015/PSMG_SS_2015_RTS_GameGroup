using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class ImpOutOfSightIconController : MonoBehaviour
    {
        private Image icon;
        private Image pointerUp;
        private Image pointerDown;
        private Image pointerLeft;
        private Image pointerRight;

        public void Awake()
        {
            icon = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon");

            pointerUp = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerUp");

            pointerDown = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerDown");

            pointerLeft = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerLeft");

            pointerRight = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerRight");
            
        }



        
    }
}