using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Managers.UIManagerAndServices;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class Event : MonoBehaviour
    {
        public string Message { get; set; }

        public void Awake()
        {
            Message = "";
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.isTrigger) return;
            if (collider.gameObject.GetComponent<ImpController>() == null) return;

            UIMessageService.Instance.CreateSimpleTextMessage(Message);
            Destroy(this);
        }

    }
}