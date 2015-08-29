using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;

namespace Assets.Scripts.LevelScripts
{
    public class Level02Events : LevelEvents
    {
        public Event Level02Started { get; private set; }
        public Event Darkness { get; private set; }
        public Event SunglassesCollected { get; private set; }
        public Event BuzzwaspSpotted { get; private set; }

        protected override void RegisterEvents()
        {
            Level02Started = gameObject.AddComponent<Event>();
            Level02Started.Message = "Kommandant, bald lassen wir das Rotgebirge hinter uns. Nur noch ein kleines Stück. Gebt Acht. Der Weg ist tückisch.";
            Level02Started.Action = Level02Action;

            Darkness = gameObject.AddComponent<Event>();
            Darkness.Message = "Es ist Nacht und die Sicht ist schlecht. Unsere Feuerteufel können die Umgebung ausleuchten und Laternen anzünden.";
            Darkness.Action = DarknessAction;

            SunglassesCollected = Events.First(e => e.Nr == 3);
            SunglassesCollected.Message = "Mein Gebieter, ihr könnt nun Kraftprotze ausbilden, die andere Kobolde werfen und schwere Gegenstände aufheben können.";
            SunglassesCollected.Action = SunglassesCollectedAction;

            BuzzwaspSpotted = Events.First(e => e.Nr == 4);
            BuzzwaspSpotted.Message = "Eine Brummwespe. Wuahhh, Wespengift und spitze Stacheln. Aber sie kann uns auf die andere Seite tragen.";
            BuzzwaspSpotted.Action = BuzzwaspAction;
        }

        private void BuzzwaspAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl2_05);
        }

        private void DarknessAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl2_02);
            ImpManager.Instance.SpawnFireBugInLvl02Start();
        }

        private void Level02Action()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl2_01);
        }

        private void SunglassesCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[5] += 1;
            ImpManager.Instance.NotifyMaxProfessions();
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl2_04);
        }
    }
}