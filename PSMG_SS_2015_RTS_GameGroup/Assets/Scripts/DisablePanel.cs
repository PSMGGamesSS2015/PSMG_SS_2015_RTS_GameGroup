using UnityEngine;

namespace Assets.Scripts
{
    public class DisablePanel : MonoBehaviour {

        private GameObject levelSelectPanel;

        public void Start () {            
            levelSelectPanel = gameObject;
            gameObject.SetActive(false);
        }

        public void ToggleActive()
        {
            levelSelectPanel.SetActive(!levelSelectPanel.activeSelf);
        }
    }
}
        