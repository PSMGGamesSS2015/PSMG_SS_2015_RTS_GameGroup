using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIMessageService : MonoBehaviour
    {

        public const float SimpleTextMessageDuration = 5f;
        public const float SpeechBubbleMessageDuration = 10f;

        public const int MaxSimpleMessages = 1;
        public const int MaxSpeechBubbleMessages = 3;

        private List<GameObject> speechBubbleMessages;

        public GameObject SimpleTextMessagePrefab;
        public GameObject SpeechBubbleMessagePrefab;

        public void Awake()
        {
            speechBubbleMessages = new List<GameObject>();
        }
        
        public void OnLevelWasLoaded(int level)
        {
            speechBubbleMessages.ForEach(Destroy);
        }

        public void CreateSimpleTextMessage(string message)
        {
            StartCoroutine(SimpleTextMessageRoutine(message));
        }

        private IEnumerator SimpleTextMessageRoutine(string message)
        {

            var canvas = GetComponent<UIManager>().CurrentUserInterface.UICanvas;

            var msg = Instantiate(SimpleTextMessagePrefab);
            msg.GetComponent<Text>().text = message;

            msg.transform.SetParent(canvas.transform, false); // set canvas as parent element

            msg.transform.position = new Vector3( // position message within canvas
                Screen.width / 2f,
                Screen.height / 2.0f,
                canvas.transform.position.z
                );

            yield return new WaitForSeconds(SimpleTextMessageDuration);

            Destroy(msg);
        }

        public void CreateSpeechBubbleMessage(string message, Speaker speaker)
        {
            CheckForMaxListSize();

            var msg = Instantiate(SpeechBubbleMessagePrefab);

            speechBubbleMessages.Add(msg);

            ConfigureMessageTextAndImage(message, speaker, msg);

            PositionElementWithinCanvas(msg);

            Counter.SetCounter(gameObject, SpeechBubbleMessageDuration, UpdateDisplayedList, false);
        }

        private void ConfigureMessageTextAndImage(string message, Speaker speaker, GameObject msg)
        {
            SetMessageText(message, msg);

            var nameOfSpeaker = "";

            switch (speaker)
            {
                case Speaker.Knight:
                    nameOfSpeaker = "Knight_Image";
                    break;
                case Speaker.Wilbur:
                    nameOfSpeaker = "Wilbur_Image";
                    break;
                case Speaker.Koboldigunde:
                    nameOfSpeaker = "Koboldigunde_Image";
                    break;
                case Speaker.KruemelBart:
                    nameOfSpeaker = "Kruemelbart_Image";
                    break;
                case Speaker.Troll:
                    nameOfSpeaker = "Troll_Image";
                    break;
            }

            SelectSpeakerImage(msg, nameOfSpeaker);
        }

        private static void SetMessageText(string message, GameObject msg)
        {
            msg.GetComponentsInChildren<Text>().ToList().First(t => t.gameObject.name == "Text").text = message;
        }

        private void CheckForMaxListSize()
        {
            if (speechBubbleMessages.Count < MaxSpeechBubbleMessages) return;
            speechBubbleMessages.Remove(speechBubbleMessages[0]); // remove oldest item
            speechBubbleMessages.Sort(); // sort items
        }

        private static void SelectSpeakerImage(GameObject msg, string nameOfSpeaker)
        {
            var img =
                msg.GetComponentsInChildren<Image>().ToList().First(sr => sr.gameObject.name == nameOfSpeaker);
            img.enabled = true;
        }

        private void UpdateDisplayedList()
        {
            if (speechBubbleMessages.Count <= 1) return;

            var oldestSpeechBubbleMessage = speechBubbleMessages[0];
            speechBubbleMessages.Remove(speechBubbleMessages[0]);
            Destroy(oldestSpeechBubbleMessage);

            speechBubbleMessages.Where(sbm => sbm != null).ToList().Sort();
            speechBubbleMessages.Where(sbm => sbm != null).ToList().ForEach(PositionElementWithinCanvas);
            
        }

        private void PositionElementWithinCanvas(GameObject msg)
        {
            var canvas = GetComponent<UIManager>().CurrentUserInterface.UICanvas;
            var screenPositionOfCanvas = canvas.transform.position;


            msg.transform.SetParent(canvas.transform, false); // set canvas as parent element

            msg.transform.position = new Vector3( // position message within canvas
                screenPositionOfCanvas.x + Screen.width/2f - 280,
                screenPositionOfCanvas.y + Screen.height/2.0f -  75 * speechBubbleMessages.Count-1,
                screenPositionOfCanvas.z
                );
        }

        public void Reset()
        {
            speechBubbleMessages.Clear();
        }
    }
}