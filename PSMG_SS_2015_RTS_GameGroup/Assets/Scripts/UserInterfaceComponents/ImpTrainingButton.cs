using System.Linq;
using Assets.Scripts.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterfaceComponents
{
    public class ImpTrainingButton : MonoBehaviour
    {
        private const string CounterName = "Counter";

        public void Awake()
        {
            Button = GetComponent<Button>();
            Counter = GetComponentsInChildren<Text>().First(t => t.gameObject.name == CounterName);
        }

        public Button Button { get; private set; }
        public Text Counter { get; set; }

        public ImpType ImpType;
    }
}