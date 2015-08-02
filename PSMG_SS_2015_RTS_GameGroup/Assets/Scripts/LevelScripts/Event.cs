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

        public void Awake()
        {
            Message = "";
            Action = null;
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.isTrigger) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;

            UIManager.Instance.UIMessageService.CreateSpeechBubbleMessage(Message, Speaker.Wilbur);

            if (Action != null)
            {
                Action();
            }
            
            Destroy(this);
        }

    }
}