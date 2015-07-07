using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// The only task of the Loader is to load the GameManager when the application starts.
    /// </summary>

    public class Loader : MonoBehaviour {

        public GameObject GameManager;

        public void Awake()
        {
            Instantiate(GameManager);
        }

    }
}
