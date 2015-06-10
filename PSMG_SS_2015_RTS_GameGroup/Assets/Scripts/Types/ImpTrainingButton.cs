using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImpTrainingButton : MonoBehaviour
{
    public Text counter;
    public Button button;

    private void Awake()
    {
        RetrieveComponents();
    }

    private void RetrieveComponents()
    {
        button = GetComponent<Button>();
        counter = GetComponentInChildren<ButtonCounter>().gameObject.GetComponent<Text>();
    }

}