using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private InputManagerListener listener;

    public interface InputManagerListener
    {
        void DisplayImpLabels();
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
            }
        }
    }

    private void DisplayImpLabels()
    {
        //Userinterface display Implabels
        listener.DisplayImpLabels();
    }

    public void RegisterListener(InputManagerListener listener)
    {
        this.listener = listener;
    }
}
