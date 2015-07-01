using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class Level01Events : MonoBehaviour
    {

        private List<GameObject> events; 

        private Event mapStartetMessage;
        private Event shieldCarrierBlockingMessage;
        private Event shieldCarrierHeavyMessage;
        private Event laddersNeededMessage;
        private Event laddersCollectedMessage;
        private Event weaponsCollectedMessage;
        private Event bombNeededMessage;
        private Event trollMetMessage;

        public void Awake()
        {
            events = GameObject.FindGameObjectsWithTag(TagReferences.Event).ToList();
        }

        public void Start()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            mapStartetMessage = SearchEvent("MapStartedMessageTrigger");

            mapStartetMessage.Message = "Macht euch auf die Socken! Die Prinzessin wurde entführt.";

            shieldCarrierBlockingMessage = SearchEvent("ShieldCarrierBlockingMessageTrigger");

            shieldCarrierBlockingMessage.Message = "Feiglinge verschanzen sich hinter ihren dicken Holzschilden. Daran kommt man nicht so leicht vorbei";

            shieldCarrierHeavyMessage = SearchEvent("ShieldCarrierHeavyMessageTrigger");

            shieldCarrierHeavyMessage.Message =
                "Unsere Holzschilde sind wuchtig und schwer. Stellt mehrere Feiglinge auf eine Oberfläche, um bestimmte Effekte zu bewirken.";

            laddersNeededMessage = SearchEvent("LaddersNeededMessageTrigger");

            laddersNeededMessage.Message = "Legt Leitern über die Abgründe, um auf die andere Seite zu kommen.";

            laddersCollectedMessage = SearchEvent("LaddersCollectedMessageTrigger");

            laddersCollectedMessage.Message = "Wir haben einen Haufen Leitern gefunden.";

            laddersCollectedMessage.Action = LaddersCollectedAction;

            weaponsCollectedMessage = SearchEvent("WeaponsCollectedMessageTrigger");

            weaponsCollectedMessage.Message = "Pieks pieks, bumm bumm.";

            weaponsCollectedMessage.Action = WeaponsCollectedAction;

            bombNeededMessage = SearchEvent("BombNeededMessageTrigger");

            bombNeededMessage.Message = "Der Fels ist brüchig. Mit einer Bombe können wir in wegsprengen.";

            trollMetMessage = SearchEvent("TrollMetMessageTrigger");

            trollMetMessage.Message = "Ein Troll! Vorsichtig, die sind stark und schnell sauer!";
        }

        private Event SearchEvent(string objectName)
        {
            var result = events.FirstOrDefault(e => e.gameObject.name.Equals(objectName));
            return result != null ? result.GetComponent<Event>() : null;
        }

        private void LaddersCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[2] += 10;
            // TODO Refactor; this smells
            ImpManager.Instance.NotifyMaxProfessions();
        }

        private void WeaponsCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[0] += 10;
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[3] += 10;
            // TODO Refactor; this smells
            ImpManager.Instance.NotifyMaxProfessions();
        }
    }
}