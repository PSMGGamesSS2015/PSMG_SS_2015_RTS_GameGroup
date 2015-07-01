using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIMessageService : MonoBehaviour
    {

        public GameObject SimpleTextMessagePrefab;
        public GameObject SpeechBubbleMessagePrefab;

        public static UIMessageService Instance;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void CreateSimpleTextMessage(string message)
        {
            StartCoroutine(SimpleTextMessageroutine(message));
        }

        private IEnumerator SimpleTextMessageroutine(string message)
        {
            var msg = Instantiate(SimpleTextMessagePrefab);
            msg.GetComponent<Text>().text = message;

            msg.transform.parent = this.transform;
            msg.transform.localPosition = new Vector3(this.transform.position.x - GetComponent<RectTransform>().rect.width / 2f, this.transform.position.y - GetComponent<RectTransform>().rect.height / 12f, this.transform.position.z);

            yield return new WaitForSeconds(4f);

            Destroy(msg);
        }

        public void CreateSpeechBubbleMessage(string message)
        {
            
        }

    }
}