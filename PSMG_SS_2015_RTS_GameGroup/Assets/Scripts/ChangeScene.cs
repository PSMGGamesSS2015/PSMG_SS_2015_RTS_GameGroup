using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Managers
{

    public class ChangeScene : MonoBehaviour
    {

        private GameObject gameManagerObject;
        public GameManager gameManager;
        // Use this for initialization
        void Start()
        {
            gameManagerObject = GameObject.Find("GameManager(Clone)");
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeLevel(int sceneNumber)
        {
            gameManager.LoadLevel(sceneNumber);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
