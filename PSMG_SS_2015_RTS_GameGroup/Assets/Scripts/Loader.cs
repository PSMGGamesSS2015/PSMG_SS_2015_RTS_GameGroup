using UnityEngine;
using System.Collections;

/// <summary>
/// The only task of the Loader is to load the GameManager when the application starts.
/// </summary>

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    
    private void Awake()
    {
        Instantiate(gameManager);
    }

}
