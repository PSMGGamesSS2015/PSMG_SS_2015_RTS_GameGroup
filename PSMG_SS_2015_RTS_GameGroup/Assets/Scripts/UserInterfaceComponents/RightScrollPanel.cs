using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UserInterfaceComponents
{
    public class RightScrollPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isMouseOverPanel;

        public void Awake()
        {
            isMouseOverPanel = false;
        }

        public void FixedUpdate()
        {
            if (isMouseOverPanel)
            {
                InputManager.Instance.MoveCameraRight();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOverPanel = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOverPanel = false;
        }
    }
}