using System.Collections.Generic;
using Assets.Scripts.Types;
using Assets.Scripts.UserInterface;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// The input managers defines the semantic of in-game input events and
    /// forwards these events to its listeners.
    /// It registers input from the keyboard as well as from the user interface.
    /// </summary>

    public class InputManager : MonoBehaviour, UIManager.IUIManagerListener {

        private List<IInputManagerListener> listeners;
        private bool isPaused;
        // ReSharper disable once NotAccessedField.Local
        private UserInterface.UserInterface userInterface;

        public void Awake()
        {
            isPaused = false;
            listeners = new List<IInputManagerListener>();
        }

        public interface IInputManagerListener
        {
            void OnDisplayImpLabels();
            void OnProfessionSelected(ImpType profession);
            void OnSelectNextImp();
            void OnDismissImpLabels();
        }

        public void OnGUI()
        {
            var e = Event.current;
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
            if (e.type != EventType.KeyUp) return;
            switch (e.keyCode) {
                case KeyCode.LeftAlt:
                    DismissImpLabels();
                    break;
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
            foreach (var listener in listeners)
            {
                listener.OnDismissImpLabels();
            }
        }

        private void SelectNextImp()
        {
            foreach (var listener in listeners)
            {
                listener.OnSelectNextImp();
            }
        }

        private void SelectProfession(ImpType impType)
        {
            foreach (var listener in listeners)
            {
                listener.OnProfessionSelected(impType);
            }
        }

        private void SelectProfession(int impTypeNumber)
        {
            foreach (var listener in listeners)
            {
                listener.OnProfessionSelected((ImpType) impTypeNumber);
            }
        }

        private void DisplayImpLabels()
        {
            foreach (var listener in listeners)
            {
                listener.OnDisplayImpLabels();
            }
        }

        public void RegisterListener(IInputManagerListener listener)
        {
            listeners.Add(listener);
        }

        private void RegisterListenersForImpTrainingButtons(UserInterface.UserInterface userInteface)
        {
            ImpTrainingButton[] buttons = userInteface.ImpTrainingButtons;
            for (var i = 0; i < buttons.Length; i++)
            {
                var nr = i;
                buttons[nr].Button.onClick.AddListener(() => SelectProfession(nr));
            }
        }

        void UIManager.IUIManagerListener.OnUserInterfaceLoaded(UserInterface.UserInterface userInteface)
        {
            this.userInterface = userInteface;
            RegisterListenersForImpTrainingButtons(userInteface);
        }

    }
}
