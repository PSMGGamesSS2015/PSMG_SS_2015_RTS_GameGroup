using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    Button[] impTrainingButtons;
    public Button[] ImpTrainingButtons
    {
        get
        {
            return impTrainingButtons;
        }
    }

    private void Awake()
    {
        GameObject[] buttonGameObjects = GameObject.FindGameObjectsWithTag(TagReferences.UI_IMP_TRAINING_BUTTON);
        impTrainingButtons = new Button[buttonGameObjects.Length];
        for (int i = 0; i < impTrainingButtons.Length; i++)
        {
            impTrainingButtons[i] = buttonGameObjects[i].GetComponent<Button>();
        }
    }

}