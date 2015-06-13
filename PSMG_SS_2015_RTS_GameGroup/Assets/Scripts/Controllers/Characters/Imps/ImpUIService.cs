using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    public class ImpUIService : MonoBehaviour
    {
        private bool areLabelsDisplayed;

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
    }
}