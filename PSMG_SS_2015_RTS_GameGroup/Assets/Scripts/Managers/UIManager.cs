using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{

    private Button[] buttonBar;

    public interface UIManagerListener
    {
        void OnTrainImpClick(ImpType job);
    }

    public void SetButtonBar(Button[] buttonBar)
    {
        this.buttonBar = buttonBar;
        for (int i = 0; i < buttonBar.Length; i++)
        {
            Debug.Log(buttonBar[i]);
            buttonBar[i].onClick.AddListener(this.ButtonClicked);
        }

    }

    private void ButtonClicked()
    {
        Debug.Log("A button was clicked");
    }

}