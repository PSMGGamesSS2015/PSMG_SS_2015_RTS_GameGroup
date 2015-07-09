using UnityEngine;
using System.Collections;

public class DisablePanel : MonoBehaviour {

    private GameObject levelSelectPanel;

	// Use this for initialization
	void Start () {            
        levelSelectPanel = gameObject;
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleActive()
        {
            if (levelSelectPanel.activeSelf)
            {
                levelSelectPanel.SetActive(false);
            }
            else
            {
                levelSelectPanel.SetActive(true);
            }
                
        }
}
        