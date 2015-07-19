using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts
{

    public class ChangeScene : MonoBehaviour
    {

        public void ChangeLevel(int sceneNumber)
        {
            LevelManager.Instance.LoadLevel(sceneNumber);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
