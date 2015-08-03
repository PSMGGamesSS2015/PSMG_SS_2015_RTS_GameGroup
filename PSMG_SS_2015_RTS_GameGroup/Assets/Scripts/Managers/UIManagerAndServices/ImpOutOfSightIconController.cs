using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class ImpOutOfSightIconController : MonoBehaviour
    {
        private Image icon;
        public Image pointerUp;
        public Image pointerDown;
        public Image pointerLeft;
        public Image pointerRight;

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