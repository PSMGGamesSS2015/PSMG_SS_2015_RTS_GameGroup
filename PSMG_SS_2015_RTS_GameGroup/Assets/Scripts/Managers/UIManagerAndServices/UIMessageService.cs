using System;
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
        private bool isCheckingForMaxListSize;

        public void Awake()
        {
            isCheckingForMaxListSize = false;
            speechBubbleMessages = new List<GameObject>();
        }

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
            CheckForMaxListSize();

            var msg = Instantiate(SpeechBubbleMessagePrefab);
            speechBubbleMessages.Add(msg);

            ConfigureMessageTextAndImage(message, speaker, msg);

            positionElementWithinCanvas(msg);
            
            Counter.SetCounter(gameObject, SpeechBubbleMessageDuration, UpdateDisplayedList, msg, false);
        }

        private void ConfigureMessageTextAndImage(string message, Speaker speaker, GameObject msg)
        {
            

            SetMessageText(message, msg);

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

            SelectSpeakerImage(msg, nameOfSpeaker);
        }

        private static void SetMessageText(string message, GameObject msg)
        {
            msg.GetComponentsInChildren<Text>().ToList().First(t => t.gameObject.name == "Text").text = message;
        }

        private void CheckForMaxListSize()
        {
            if (isCheckingForMaxListSize) return;
            isCheckingForMaxListSize = true;
            if (speechBubbleMessages.Count < MaxSpeechBubbleMessages) return;
            speechBubbleMessages.Remove(speechBubbleMessages[0]); // remove oldest item
            speechBubbleMessages.Sort(); // sort items
            isCheckingForMaxListSize = false;
        }

        private static void SelectSpeakerImage(GameObject msg, string nameOfSpeaker)
        {
            var img =
                msg.GetComponentsInChildren<Image>().ToList().First(sr => sr.gameObject.name == nameOfSpeaker);
            img.enabled = true;
        }

        private void UpdateDisplayedList(GameObject msg)
        {
            speechBubbleMessages.Remove(msg);
            speechBubbleMessages.Sort();
            speechBubbleMessages.ForEach(positionElementWithinCanvas);

            Destroy(msg);
        }

        private void positionElementWithinCanvas(GameObject msg)
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
    }
}