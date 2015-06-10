using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour, ImpManager.ImpManagerListener
{
    private ImpTrainingButton[] impTrainingButtons;
    private int[] currentMaxProfessions;

    public ImpTrainingButton[] ImpTrainingButtons
    {
        get
        {
            return impTrainingButtons;
        }
    }

    private void Awake()
    {
        RetrieveComponents();
    }

    private void RetrieveComponents()
    {
        impTrainingButtons = GetComponentsInChildren<ImpTrainingButton>();
    }

    public void Setup(LevelConfig config)
    {
        currentMaxProfessions = config.MaxProfessions;
    }

    private void Start()
    {
        if (currentMaxProfessions != null)
        {
            for (int i = 0; i < impTrainingButtons.Length; i++)
            {
                impTrainingButtons[i].counter.text = currentMaxProfessions[i].ToString();
            }
        }
    }

    void ImpManager.ImpManagerListener.OnUpdateMaxProfessions(int[] professions)
    {
        for (int i = 0; i < impTrainingButtons.Length; i++)
        {
            impTrainingButtons[i].counter.text = (currentMaxProfessions[i] - professions[i]).ToString();
        }
    }
}