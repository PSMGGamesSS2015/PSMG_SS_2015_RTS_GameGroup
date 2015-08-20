using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.LevelScripts
{
    public class Level05Events : LevelEvents
    {
        private Event levelStartedMessage;
        private Event closedDoorMessage;
        private Event torchesCollectedMessage;
        private Event potionCollectedMessage;
        private Event canonFiringMessage;

        protected override void RegisterEvents()
        {
            levelStartedMessage = events.First(e => e.Nr == 1);
            levelStartedMessage.Message =
                "Mhh, hier geht’s nicht weiter, mein Herr. Wir müssen einen anderen Weg finden!";
            levelStartedMessage.Action = levelStartedAction;


            closedDoorMessage = events.First(e => e.Nr == 2);
            closedDoorMessage.Message = "Wir haben einen Haufen Leitern gefunden.";
            closedDoorMessage.Action = closedDoorAction;


            torchesCollectedMessage = events.First(e => e.Nr == 3);
            torchesCollectedMessage.Message = "Fackeln… guuut. Sie brennen hell und heiß. Die werden wir noch brauchen!";
            torchesCollectedMessage.Action = torchesCollectedAction;


            potionCollectedMessage = events.First(e => e.Nr == 4);
            potionCollectedMessage.Message =
                "Sieht aus wie eine Art Zaubertrank. Ein kleines Wildschwein ist auf der Flasche abgebildet. (Grunz Grunz). Laut Etikett soll er übermenschliche Kräfte verleihen!";
            potionCollectedMessage.Action = potionCollectedAction;


            canonFiringMessage = events.First(e => e.Nr == 5);
            canonFiringMessage.Message = "Achtung, wir stehen unter Feueeer!!! In Deckung!";
            canonFiringMessage.Action = canonFiringAction;
        }

        private void canonFiringAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLVl5_05);
        }

        private void potionCollectedAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl5_04);
        }

        private void torchesCollectedAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl5_03);
        }

        private void closedDoorAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl5_02);
        }

        private void levelStartedAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl5_01);
        }
    }
}