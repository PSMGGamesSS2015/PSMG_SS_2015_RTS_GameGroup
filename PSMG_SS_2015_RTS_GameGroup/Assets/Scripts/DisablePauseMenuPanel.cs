using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    public class DisablePauseMenuPanel : MonoBehaviour {

        public void Start () {
            GameManager.Instance.GetComponent<InputManager>().PauseGame();
        }
	
        public void ToggleActive()
        {
            GameManager.Instance.GetComponent<InputManager>().PauseGame();
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
        