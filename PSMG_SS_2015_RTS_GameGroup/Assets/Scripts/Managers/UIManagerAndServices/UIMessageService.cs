using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIMessageService : MonoBehaviour
    {

        public const float SimpleTextMessageDuration = 5f;
        public const float SpeechBubbleMessageDuration = 5f;

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
                pos.z
                );

            yield return new WaitForSeconds(SimpleTextMessageDuration);

            Destroy(msg);
        }

        public void CreateSpeechBubbleMessage(string message, Speaker speaker)
        {
            StartCoroutine((SpeechBubbleMessageRoutine(message, speaker)));
        }

        private IEnumerator SpeechBubbleMessageRoutine(string message, Speaker speaker)
        {
            var msg = Instantiate(SpeechBubbleMessagePrefab);

            var text = 
                msg.GetComponentsInChildren<Text>().ToList().First(t => t.gameObject.name == "Text");

            text.text = message;
            
            var speakerLabel =
                msg.GetComponentsInChildren<Text>().ToList().First(sr => sr.gameObject.name == "Speaker_Label");

            var nameOfSpeaker = "";
            switch (speaker)
            {
                case Speaker.Knight:
                    nameOfSpeaker = "Knight_Image";
                    speakerLabel.text = "Knight";
                    break;
                case Speaker.Wilbur:
                    nameOfSpeaker = "Wilbur_Image";
                    speakerLabel.text = "Wilbur";
                    break;
                case Speaker.Koboldigunde:
                    nameOfSpeaker = "Koboldigunde_Image";
                    speakerLabel.text = "Koboldigunde";
                    break;
                case Speaker.KruemelBart:
                    nameOfSpeaker = "Kruemelbart_Image";
                    speakerLabel.text = "Kruemelbart";
                    break;
                case Speaker.Troll:
                    nameOfSpeaker = "Troll_Image";
                    speakerLabel.text = "Troll";
                    break;
            }

            var img =
                msg.GetComponentsInChildren<Image>().ToList().First(sr => sr.gameObject.name == nameOfSpeaker);
            img.enabled = true;

            var canvas = GetComponent<UIManager>().CurrentUserInterface.UICanvas;
            var pos = canvas.transform.position;
            var width = canvas.GetComponent<RectTransform>().rect.width;

            msg.transform.SetParent(canvas.transform, false); // set canvas as parent element

            msg.transform.localPosition = new Vector3( // position message within canvas
                pos.x - width / 20f,
                pos.y + 85, 
                pos.z
                );

            yield return new WaitForSeconds(SpeechBubbleMessageDuration);

            Destroy(msg);
        }
    }
}