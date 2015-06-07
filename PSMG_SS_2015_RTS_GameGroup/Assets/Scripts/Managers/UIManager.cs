using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    
    private List<UIManagerListener> listeners;

    private void Awake()
    {
        listeners = new List<UIManagerListener>();
    }

    public interface UIManagerListener
    {
        void OnProfessionSelected(ImpType profession);        
    }

    private void SelectProfession(ImpType impType)
    {
        foreach (UIManagerListener listener in listeners)
        {
            listener.OnProfessionSelected(impType);
        }
    }

    public void UIInput(int buttonNumber)
    {

        switch (buttonNumber)
        {
            case 1:
                SelectProfession(ImpType.Spearman);
                break;
            case 2:
                SelectProfession(ImpType.Coward);
                break;
            case 3:
                SelectProfession(ImpType.LadderCarrier);
                break;
            case 4:
                SelectProfession(ImpType.Blaster);
                break;
            case 5:
                SelectProfession(ImpType.Firebug);
                break;
            case 6:
                SelectProfession(ImpType.Botcher);
                break;
            case 7:
                SelectProfession(ImpType.Schwarzenegger);
                break;
            case 0:
                SelectProfession(ImpType.Unemployed);
                break;
        }
    }


    public void RegisterListener(UIManagerListener listener)
    {
        listeners.Add(listener);
    }
}