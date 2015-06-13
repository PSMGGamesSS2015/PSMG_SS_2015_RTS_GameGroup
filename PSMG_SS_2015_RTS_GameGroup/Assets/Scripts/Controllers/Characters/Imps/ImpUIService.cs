using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpUIService : MonoBehaviour
    {
        private bool areLabelsDisplayed;

        public ImpSelection Selection { get; private set; }

        public void Awake()
        {
            Selection = GetComponentInChildren<ImpSelection>();
        }

        public void OnGUI()
        {
            if (areLabelsDisplayed)
            {
                Handles.Label(new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y + 0.75f, 0), GetComponent<ImpTrainingService>().Type.ToString());
            }
        }

        public void DisplayLabel()
        {
            areLabelsDisplayed = true;
        }

        public void DismissLabel()
        {
            areLabelsDisplayed = false;
        }

        public void OnMouseDown()
        {
            var impController = GetComponent<ImpController>();
            foreach (var listener in impController.Listeners)
            {
                listener.OnImpSelected(impController);
            }
        }
    }
}