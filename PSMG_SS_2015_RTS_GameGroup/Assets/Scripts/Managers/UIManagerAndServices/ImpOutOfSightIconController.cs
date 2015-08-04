using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class ImpOutOfSightIconController : MonoBehaviour
    {
        public Image Icon_Default { get; private set; }
        public Image Icon_Spearman { get; private set; }
        public Image Icon_Coward { get; private set; }
        public Image Icon_LadderCarrier { get; private set; }
        public Image Icon_Blaster { get; private set; }
        public Image Icon_Firebug { get; private set; }
        public Image Icon_Schwarzenegger { get; private set; }

        public Image pointerUp { get; private set; }
        public Image pointerDown { get; private set; }
        public Image pointerLeft { get; private set; }
        public Image pointerRight { get; private set; }

        public void Awake()
        {
            Icon_Default = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_Default");

            Icon_Spearman = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_Spearman");

            Icon_Coward = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_Coward");

            Icon_LadderCarrier = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_LadderCarrier");

            Icon_Blaster = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_Blaster");

            Icon_Firebug = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_Firebug");

            Icon_Schwarzenegger = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "Icon_Schwarzenegger");
            
            pointerUp = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerUp");

            pointerDown = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerDown");

            pointerLeft = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerLeft");

            pointerRight = GetComponentsInChildren<Image>().ToList().First(img => img.gameObject.name == "PointerRight");
            
        }



        
    }
}