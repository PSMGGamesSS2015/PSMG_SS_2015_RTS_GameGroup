using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The input managers defines the semantic of in-game input events and
/// forwards these events to its listeners.
/// </summary>

public class InputManager : MonoBehaviour {

    private List<InputManagerListener> listeners;

    private void Awake()
    {
        listeners = new List<InputManagerListener>();
    }

    public interface InputManagerListener
    {
        void OnDisplayImpLabels();
        void OnProfessionSelected(ImpType profession);
        void OnSelectNextImp();
        void OnDismissImpLabels();
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            switch (e.keyCode)
            {
                case KeyCode.LeftAlt:
                    DisplayImpLabels();
                    break;
                case KeyCode.Tab:
                    SelectNextImp();
                    break;
                case KeyCode.Alpha1:
                    SelectProfession(ImpType.Spearman);
                    break;
                case KeyCode.Alpha2:
                    SelectProfession(ImpType.Coward);
                    break;
                case KeyCode.Alpha3:
                    SelectProfession(ImpType.Pest);
                    break;
                case KeyCode.Alpha4:
                    SelectProfession(ImpType.LadderCarrier);
                    break;
                case KeyCode.Alpha5:
                    SelectProfession(ImpType.Blaster);
                    break;
                case KeyCode.Alpha6:
                    SelectProfession(ImpType.Firebug);
                    break;
                case KeyCode.Alpha7:
                    SelectProfession(ImpType.Minnesinger);
                    break;
                case KeyCode.Alpha8:
                    SelectProfession(ImpType.Botcher);
                    break;
                case KeyCode.Alpha9:
                    SelectProfession(ImpType.Schwarzenegger);
                    break;
                case KeyCode.Alpha0:
                    SelectProfession(ImpType.Unemployed);
                    break;
            }
        }
        if (e.type == EventType.KeyUp)
        {
            switch (e.keyCode) {
                case KeyCode.LeftAlt:
                    DismissImpLabels();
                    break;
            }
            
        }
    }

    private void DismissImpLabels()
    {
        foreach (InputManagerListener listener in listeners)
        {
            listener.OnDismissImpLabels();
        }
    }

    private void SelectNextImp()
    {
        foreach (InputManagerListener listener in listeners)
        {
            listener.OnSelectNextImp();
        }
    }

    private void SelectProfession(ImpType impType)
    {
        foreach (InputManagerListener listener in listeners)
        {
            listener.OnProfessionSelected(impType);
        }
    }

    private void DisplayImpLabels()
    {
        foreach (InputManagerListener listener in listeners)
        {
            listener.OnDisplayImpLabels();
        }
    }

    public void RegisterListener(InputManagerListener listener)
    {
        listeners.Add(listener);
    }
}
