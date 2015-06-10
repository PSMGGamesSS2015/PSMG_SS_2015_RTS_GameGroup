using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// The input managers defines the semantic of in-game input events and
/// forwards these events to its listeners.
/// It registers input from the keyboard as well as from the user interface.
/// </summary>

public class InputManager : MonoBehaviour, UIManager.UIManagerListener {

    private List<InputManagerListener> listeners;
    private bool isPaused;
    private UserInterface userInterface;

    private void Awake()
    {
        isPaused = false;
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
                    SelectProfession(ImpType.LadderCarrier);
                    break;
                case KeyCode.Alpha4:
                    SelectProfession(ImpType.Blaster);
                    break;
                case KeyCode.Alpha5:
                    SelectProfession(ImpType.Firebug);
                    break;
                case KeyCode.Alpha6:
                    SelectProfession(ImpType.Botcher);
                    break;
                case KeyCode.Alpha7:
                    SelectProfession(ImpType.Schwarzenegger);
                    break;
                case KeyCode.Alpha0:
                    SelectProfession(ImpType.Unemployed);
                    break;
                case KeyCode.Space:
                    PauseGame();
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

    private void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
        } else {
            Time.timeScale = 0f;
            isPaused = true;
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

    private void SelectProfession(int impTypeNumber)
    {
        foreach (InputManagerListener listener in listeners)
        {
            listener.OnProfessionSelected((ImpType) impTypeNumber);
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

    private void RegisterListenersForImpTrainingButtons(UserInterface userInteface)
    {
        ImpTrainingButton[] buttons = userInteface.ImpTrainingButtons;
        for (int i = 0; i < buttons.Length; i++)
        {
            int nr = i;
            buttons[nr].button.onClick.AddListener(() => SelectProfession(nr));
        }
    }

    void UIManager.UIManagerListener.OnUserInterfaceLoaded(UserInterface userInteface)
    {
        this.userInterface = userInteface;
        RegisterListenersForImpTrainingButtons(userInteface);
    }

}
