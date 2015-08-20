using System.Linq;
using Assets.Scripts.Managers;

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
            var narrator = SoundManager.Instance.Narrator;

            levelStartedMessage = events.First(e => e.Nr == 1);
            levelStartedMessage.Message =
                "Mhh, hier geht’s nicht weiter, mein Herr. Wir müssen einen anderen Weg finden!";

            closedDoorMessage = events.First(e => e.Nr == 2);
            closedDoorMessage.Message = "Wir haben einen Haufen Leitern gefunden.";

            torchesCollectedMessage = events.First(e => e.Nr == 3);
            torchesCollectedMessage.Message = "Fackeln… guuut. Sie brennen hell und heiß. Die werden wir noch brauchen!";

            potionCollectedMessage = events.First(e => e.Nr == 4);
            potionCollectedMessage.Message =
                "Sieht aus wie eine Art Zaubertrank. Ein kleines Wildschwein ist auf der Flasche abgebildet. (Grunz Grunz). Laut Etikett soll er übermenschliche Kräfte verleihen!";

            canonFiringMessage = events.First(e => e.Nr == 5);
            canonFiringMessage.Message = "Achtung, wir stehen unter Feueeer!!! In Deckung!";
        }
    }
}