using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Managers.UIManagerAndServices;
using UnityEngine;
using System;

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

            UIMessageService.Instance.CreateSimpleTextMessage(Message);

            if (Action != null)
            {
                Action();
            }
            
            

            Destroy(this);
        }

    }
}