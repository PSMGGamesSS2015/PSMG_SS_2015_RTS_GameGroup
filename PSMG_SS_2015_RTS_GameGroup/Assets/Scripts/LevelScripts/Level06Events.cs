using System.Linq;
using Assets.Scripts.Managers;

namespace Assets.Scripts.LevelScripts
{
    public class Level06Events : LevelEvents
    {
        private Event levelStartedMessage;
        private Event flourHasFallenIntoBowlMessage;
        private Event cakeAlmostReadyMessage;
        private Event cakeReadyMessage;
        private Event knightEatingCakeMessage;
        private Event impsAreAtSuspensionBridgeMessage;
        private Event impsHaveRescuedPrincessMessage;
        private Event kingJumpsOutOfCakeMessage;

        protected override void RegisterEvents()
        {
            var narrator = SoundManager.Instance.Narrator;

            levelStartedMessage = events.First(e => e.Nr == 1);
            levelStartedMessage.Message = "Meister, sprengt dieses lästige Tor aus dem Weg.";

            flourHasFallenIntoBowlMessage = events.First(e => e.Nr == 2);
            flourHasFallenIntoBowlMessage.Message = "Mmh, es ist Mehl in die Schüssel gefallen. Mit Wasser und etwas zum Rühren könnten wir uns einen Kuchen backen.";

            cakeAlmostReadyMessage = events.First(e => e.Nr == 3);
            cakeAlmostReadyMessage.Message = "Das riecht fast so gut wie die Leckereien der Prinzessin, mein Herr. Er muss nur noch kurz backen.";

            cakeReadyMessage = events.First(e => e.Nr == 4);
            cakeReadyMessage.Message = "Mmmmmhh.";

            knightEatingCakeMessage = events.First(e => e.Nr == 5);
            knightEatingCakeMessage.Message = "Kuchen … hmm … nur ein kleines Stück.";
            // TODO knightEatingCakeMessage.Message = "Lasst uns schnell weitergehen, bevor der Ritter aufgegessen hat.";

            impsAreAtSuspensionBridgeMessage = events.First(e => e.Nr == 6);
            impsAreAtSuspensionBridgeMessage.Message = "Wo ist bloß die Prinzessin? Wir konnten sie nirgendwo finden. Dringt tiefer ins Verlies vor. Versucht, diese Brücke zu Fall zu bringen.";

            impsHaveRescuedPrincessMessage = events.First(e => e.Nr == 7);
            impsHaveRescuedPrincessMessage.Message = "Geschafft, geschafft, geschafft! Nun bringt die Prinzessin sicher aus dem Verlies.";

            kingJumpsOutOfCakeMessage = events.First(e => e.Nr == 8);
            kingJumpsOutOfCakeMessage.Message = "Ihr wollt meine Dienerin entführen? Niemand klaut mir meine Bäckerin!";
        }
    }
}