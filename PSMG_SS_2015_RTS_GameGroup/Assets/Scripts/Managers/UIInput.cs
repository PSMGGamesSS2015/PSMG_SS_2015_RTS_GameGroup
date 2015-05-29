using UnityEngine;
using System.Collections;

public class UIInput : MonoBehaviour
{
    public void Input(int buttonNumber)
    {
        var uiManager = GameObject.Find("GameManager(Clone)").GetComponent<UIManager>();
        uiManager.UIInput(buttonNumber);
    }
}
