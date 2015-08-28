using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class Level05Events : LevelEvents
    {
        public Event LevelStartedMessage { get; private set; }
        public Event ClosedDoorMessage { get; private set; }
        public Event TorchesCollectedMessage { get; private set; }
        public Event PotionCollectedMessage { get; private set; }
        public Event CanonFiringMessage { get; private set; }

        public GongController Gong { get; private set; }
        public DrawBridgeController DrawBridge { get; private set; }

        public new void Awake()
        {
            base.Awake();

            Gong = GameObject.FindGameObjectWithTag(TagReferences.Gong).GetComponent<GongController>();
            DrawBridge = GameObject.FindGameObjectWithTag(TagReferences.DrawBridge).GetComponent<DrawBridgeController>();
        }

        protected override void RegisterEvents()
        {
            LevelStartedMessage = Events.First(e => e.Nr == 1);
            LevelStartedMessage.Message =
                "Mhh, hier geht’s nicht weiter, mein Herr. Wir müssen einen anderen Weg finden!";
            LevelStartedMessage.Action = LevelStartedAction;


            ClosedDoorMessage = Events.First(e => e.Nr == 2);
            ClosedDoorMessage.Message = "Wir haben einen Haufen Leitern gefunden.";
            ClosedDoorMessage.Action = ClosedDoorAction;


            TorchesCollectedMessage = Events.First(e => e.Nr == 3);
            TorchesCollectedMessage.Message = "Fackeln… guuut. Sie brennen hell und heiß. Die werden wir noch brauchen!";
            TorchesCollectedMessage.Action = TorchesCollectedAction;


            PotionCollectedMessage = Events.First(e => e.Nr == 4);
            PotionCollectedMessage.Message =
                "Sieht aus wie eine Art Zaubertrank. Ein kleines Wildschwein ist auf der Flasche abgebildet. (Grunz Grunz). Laut Etikett soll er übermenschliche Kräfte verleihen!";
            PotionCollectedMessage.Action = PotionCollectedAction;


            CanonFiringMessage = Events.First(e => e.Nr == 5);
            CanonFiringMessage.Message = "Achtung, wir stehen unter Feueeer!!! In Deckung!";
            CanonFiringMessage.Action = CanonFiringAction;
        }

        private void CanonFiringAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLVl5_05);
        }

        private void PotionCollectedAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl5_04);
        }

        private void TorchesCollectedAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl5_03);
        }

        private void ClosedDoorAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl5_02);
        }

        private void LevelStartedAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl5_01);
        }
    }
}