using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.LevelScripts
{
    public class Level02Events : LevelEvents
    {
        private Event level02Started;
        private Event darkness;
        private Event ravineReached;
        private Event sunglassesCollected;
        private Event buzzwaspSpotted;
        private Event reachedGoal;

        protected override void RegisterEvents()
        {
            level02Started = events.First(e => e.Nr == 1);
            level02Started.Message = "Kommandant, bald lassen wir das Rotgebirge hinter uns. Nur noch ein kleines Stück. Gebt Acht. Der Weg ist tückisch.";
            level02Started.Action = level02Action;


            darkness = events.First(e => e.Nr == 2);
            darkness.Message = "Es ist Nacht und die Sicht ist schlecht. Unsere Feuerteufel können die Umgebung ausleuchten und Laternen anzünden.";
            darkness.Action = darknessAction;


            ravineReached = events.First(e => e.Nr == 3);
            ravineReached.Message = "Diese Schlucht sieht weit und tief aus. Unsere Leitern reichen nicht bis zur anderen Seite. Aber ein Kraftprotz könnte andere Kobolde hinüberwerfen. Sucht nach einem Zaubertrank, um Kraftprotze auszubilden.";
            ravineReached.Action = ravineReachedAction;


            sunglassesCollected = events.First(e => e.Nr == 4);
            sunglassesCollected.Message = "Mein Gebieter, ihr könnt nun Kraftprotze ausbilden, die andere Kobolde werfen und schwere Gegenstände aufheben können.";
            sunglassesCollected.Action = SunglassesCollectedAction;


            buzzwaspSpotted = events.First(e => e.Nr == 5);
            buzzwaspSpotted.Message = "Eine Brummwespe. Wuahhh, Wespengift und spitze Stacheln. Aber sie kann uns auf die andere Seite tragen.";
            buzzwaspSpotted.Action = buzzwaspAction;


            reachedGoal = events.First(e => e.Nr == 6);
            reachedGoal.Message = "Mehr Kuchenkrümel … Rasch, weiter!";
            reachedGoal.Action = reachedAction;
        }

        private void reachedAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl2_07);
        }

        private void buzzwaspAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl2_05);
        }

        private void ravineReachedAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl2_03);
        }

        private void darknessAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl2_02);
        }

        private void level02Action()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl2_01);
        }

        private void SunglassesCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[5] += 1;
            ImpManager.Instance.NotifyMaxProfessions();
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl2_04);
        }
    }
}