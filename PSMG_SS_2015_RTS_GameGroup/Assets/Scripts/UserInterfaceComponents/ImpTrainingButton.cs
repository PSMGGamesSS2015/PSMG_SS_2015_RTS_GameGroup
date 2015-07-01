using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterfaceComponents
{
    public class ImpTrainingButton : MonoBehaviour
    {
        public Text Counter;
        public Button Button;

        public void Awake()
        {
            RetrieveComponents();
        }

        private void RetrieveComponents()
        {
            Button = GetComponent<Button>();
            Counter = GetComponentInChildren<ButtonCounter>().gameObject.GetComponent<Text>();
        }

    }
}