using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIMessageService : MonoBehaviour
    {

        public const float SimpleTextMessageDuration = 5f;

        public GameObject SimpleTextMessagePrefab;
        public GameObject SpeechBubbleMessagePrefab;

        public void CreateSimpleTextMessage(string message)
        {
            StartCoroutine(SimpleTextMessageRoutine(message));
        }

        private IEnumerator SimpleTextMessageRoutine(string message)
        {
            var msg = Instantiate(SimpleTextMessagePrefab);
            msg.GetComponent<Text>().text = message;

            var canvas = GetComponent<UIManager>().CurrentUserInterface.UICanvas;
            var pos = canvas.transform.position;
            var width = canvas.GetComponent<RectTransform>().rect.width;
            var height = canvas.GetComponent<RectTransform>().rect.height;

            msg.transform.SetParent(canvas.transform, false); // set canvas as parent element

            msg.transform.localPosition = new Vector3( // position message within canvas
                pos.x - width / 6f, 
                pos.y - height / 6f, 
                pos.z);

            yield return new WaitForSeconds(SimpleTextMessageDuration);

            Destroy(msg);
        }

        public void CreateSpeechBubbleMessage(string message)
        {
            
        }

    }
}