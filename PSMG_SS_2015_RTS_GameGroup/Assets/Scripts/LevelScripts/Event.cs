using System;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.UIManagerAndServices;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class Event : MonoBehaviour
    {
        public string Message { get; set; }
        public Action Action { get; set; }
        public int Nr; // To be set in the editor

        public void Awake()
        {
            Message = "";
            Action = null;
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.isTrigger) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;

            PlayEvent();
        }

        public void TriggerManually()
        {
            PlayEvent();
        }

        private void PlayEvent()
        {
            UIManager.Instance.UIMessageService.CreateSpeechBubbleMessage(Message, Speaker.Wilbur);

            if (Action != null)
            {
                Action();
            }

            Destroy(this);
        }
    }
}