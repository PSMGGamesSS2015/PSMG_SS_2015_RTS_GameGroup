using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInput : MonoBehaviour
{
    public GameObject GameManager;
    public Text CounterButton1, CounterButton2, CounterButton3, CounterButton4, CounterButton5, CounterButton6, CounterButton7, CounterButton8;
    private int[] remainingProfession;
    void Start()
    {
        GameManager = GameObject.Find("GameManager(Clone)");

        CounterButton1 = GameObject.Find("Counter1").GetComponent<Text>();
        CounterButton2 = GameObject.Find("Counter2").GetComponent<Text>();
        CounterButton3 = GameObject.Find("Counter3").GetComponent<Text>();
        CounterButton4 = GameObject.Find("Counter4").GetComponent<Text>();
        CounterButton5 = GameObject.Find("Counter5").GetComponent<Text>();
        CounterButton6 = GameObject.Find("Counter6").GetComponent<Text>();
        CounterButton7 = GameObject.Find("Counter7").GetComponent<Text>();
        CounterButton8 = GameObject.Find("Counter8").GetComponent<Text>();
    }

    public void Input(int buttonNumber)
    {
        var uiManager = GameManager.GetComponent<UIManager>();
        uiManager.UIInput(buttonNumber);
        RefreshUI();
    }

    public void RefreshUI()
    {
        var impManager = GameManager.GetComponent<ImpManager>();
        int[] professionMaxNumbers = impManager.getProfessionsMax();
        int[] professionNumbers = impManager.getProfessions();

        int number1 = professionMaxNumbers[0] - professionNumbers[0];
        int number2 = professionMaxNumbers[1] - professionNumbers[1];
        int number3 = professionMaxNumbers[2] - professionNumbers[2];
        int number4 = professionMaxNumbers[3] - professionNumbers[3];
        int number5 = professionMaxNumbers[4] - professionNumbers[4];
        int number6 = professionMaxNumbers[5] - professionNumbers[5];
        int number7 = professionMaxNumbers[6] - professionNumbers[6];
        int number8 = professionMaxNumbers[7] - professionNumbers[7];

        CounterButton1.text = "" + number1;
        CounterButton2.text = "" + number2;
        CounterButton3.text = "" + number3;
        CounterButton4.text = "" + number4;
        CounterButton5.text = "" + number5;
        CounterButton6.text = "" + number6;
        CounterButton7.text = "" + number7;
        CounterButton8.text = "" + number8;

    }
}
