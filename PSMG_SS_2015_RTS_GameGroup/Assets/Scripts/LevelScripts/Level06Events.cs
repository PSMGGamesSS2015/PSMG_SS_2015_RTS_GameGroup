using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.Knight;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class Level06Events : LevelEvents
    {
        public Event LevelStartedMessage { get; private set; }
        public Event FlourHasFallenIntoBowlMessage { get; private set; }
        public Event CakeAlmostReadyMessage { get; private set; }
        public Event CakeReadyMessage { get; private set; }
        public Event KnightEatingCakeMessage { get; private set; }
        public Event ImpsHaveRescuedPrincessMessage { get; private set; }

        public KnightController KnightBehindFirstGate { get; private set; }
        public KnightController KnightAtEndOfFirstFloor { get; private set; }

        public List<FurnaceController> Furnaces { get; private set; }

        public DungeonDoorController DoorToOvens { get; private set; }
        private const string DoorToOvenName = "DoorToOvens";

        public DungeonDoorController DoorToPrincess { get; private set; }
        private const string DoorToPrincessName = "DoorToPrincess";

        public DungeonDoorController DoorToExit { get; private set; }
        private const string DoorToExitName = "DoorToExit";

        public new void Start()
        {
            base.Start();

            Furnaces = new List<FurnaceController>();

            var knights = GameObject.FindGameObjectsWithTag(TagReferences.Knight).ToList();

            KnightBehindFirstGate = knights.First(k => k.name == KnightBehindFirstGateName).GetComponent<KnightController>();
            KnightAtEndOfFirstFloor = knights.First(k => k.name == KnightAtEndOfFirstFloorName).GetComponent<KnightController>();

            var dungeonDoorGameObjects = GameObject.FindGameObjectsWithTag(TagReferences.DungeonDoor).ToList();

            DoorToOvens =
                dungeonDoorGameObjects.First(go => go.name == DoorToOvenName).GetComponent<DungeonDoorController>();

            DoorToPrincess =
                dungeonDoorGameObjects.First(go => go.name == DoorToPrincessName).GetComponent<DungeonDoorController>();

            DoorToExit =
                dungeonDoorGameObjects.First(go => go.name == DoorToExitName).GetComponent<DungeonDoorController>();

            var furanceGameObjects = GameObject.FindGameObjectsWithTag(TagReferences.Furnace).ToList();
            furanceGameObjects.ForEach(fgo => Furnaces.Add(fgo.GetComponent<FurnaceController>()));
        }

        private const string KnightAtEndOfFirstFloorName = "KnightAtEndOfFirstFloor";

        private const string KnightBehindFirstGateName = "KnightBehindFirstGate";

        protected override void RegisterEvents()
        {
            LevelStartedMessage = Events.First(e => e.Nr == 1);
            LevelStartedMessage.Message = "Meister, sprengt dieses lästige Tor aus dem Weg.";
            LevelStartedMessage.Action = LevelStartedAction;

            FlourHasFallenIntoBowlMessage = Events.First(e => e.Nr == 2);
            FlourHasFallenIntoBowlMessage.Message = "Mmh, es ist Mehl in die Schüssel gefallen. Wenn wir ihn gut rühren, könnten wir uns einen Kuchen backen.";
            FlourHasFallenIntoBowlMessage.Action = FlourHasFallenIntoBowlAction;


            CakeAlmostReadyMessage = Events.First(e => e.Nr == 3);
            CakeAlmostReadyMessage.Message = "Das riecht fast so gut wie die Leckereien der Prinzessin, mein Herr. Er muss nur noch kurz backen.";
            CakeAlmostReadyMessage.Action = CakeAlmostReadyAction;

            CakeReadyMessage = Events.First(e => e.Nr == 4);
            CakeReadyMessage.Message = "Mmmmmhh.";
            CakeReadyMessage.Action = CakeReadyAction;

            KnightEatingCakeMessage = Events.First(e => e.Nr == 5);
            KnightEatingCakeMessage.Message = "Kuchen … hmm … nur ein kleines Stück.";
            KnightEatingCakeMessage.Action = KnightEatingCakeAction;

            ImpsHaveRescuedPrincessMessage = Events.First(e => e.Nr == 7);
            ImpsHaveRescuedPrincessMessage.Message = "Geschafft, geschafft, geschafft! Nun bringt die Prinzessin sicher aus dem Verlies.";
            ImpsHaveRescuedPrincessMessage.Action = ImpsHaveRescuedPrincessAction;
        }

        private void ImpsHaveRescuedPrincessAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_07);
        }

        private void KnightEatingCakeAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_05_KnightEatingCake);
        }

        private void CakeReadyAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_04_KnightSaliva);
        }

        private void CakeAlmostReadyAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_03);
        }

        private void FlourHasFallenIntoBowlAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_02);
        }

        private void LevelStartedAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_01);
        }
    }
}