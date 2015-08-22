using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.LevelScripts
{
    public class Level06Events : LevelEvents
    {
        private Event levelStartedMessage;
        private Event flourHasFallenIntoBowlMessage;
        private Event cakeAlmostReadyMessage;
        private Event cakeReadyMessage;
        private Event knightEatingCakeMessage;
        private Event impsAreAtEndOfFirstFloorMessage;
        private Event impsHaveRescuedPrincessMessage;
        private Event kingJumpsOutOfCakeMessage;

        protected override void RegisterEvents()
        {
            levelStartedMessage = Events.First(e => e.Nr == 1);
            levelStartedMessage.Message = "Meister, sprengt dieses lästige Tor aus dem Weg.";
            levelStartedMessage.Action = LevelStartedAction;

            flourHasFallenIntoBowlMessage = Events.First(e => e.Nr == 2);
            flourHasFallenIntoBowlMessage.Message = "Mmh, es ist Mehl in die Schüssel gefallen. Mit Wasser und etwas zum Rühren könnten wir uns einen Kuchen backen.";
            flourHasFallenIntoBowlMessage.Action = FlourHasFallenIntoBowlAction;


            cakeAlmostReadyMessage = Events.First(e => e.Nr == 3);
            cakeAlmostReadyMessage.Message = "Das riecht fast so gut wie die Leckereien der Prinzessin, mein Herr. Er muss nur noch kurz backen.";
            cakeAlmostReadyMessage.Action = CakeAlmostReadyAction;


            cakeReadyMessage = Events.First(e => e.Nr == 4);
            cakeReadyMessage.Message = "Mmmmmhh.";
            cakeReadyMessage.Action = CakeReadyAction;


            knightEatingCakeMessage = Events.First(e => e.Nr == 5);
            knightEatingCakeMessage.Message = "Kuchen … hmm … nur ein kleines Stück.";
            // TODO knightEatingCakeMessage.Message = "Lasst uns schnell weitergehen, bevor der Ritter aufgegessen hat.";
            knightEatingCakeMessage.Action = KnightEatingCakeAction;


            impsAreAtEndOfFirstFloorMessage = Events.First(e => e.Nr == 6);
            impsAreAtEndOfFirstFloorMessage.Message = "Wo ist bloß die Prinzessin? Wir konnten sie nirgendwo finden. Dringt tiefer ins Verlies vor. Versucht, diese Brücke zu Fall zu bringen.";
            impsAreAtEndOfFirstFloorMessage.Action = ImpsAreAtSuspensionBridgeAction;


            impsHaveRescuedPrincessMessage = Events.First(e => e.Nr == 7);
            impsHaveRescuedPrincessMessage.Message = "Geschafft, geschafft, geschafft! Nun bringt die Prinzessin sicher aus dem Verlies.";
            impsHaveRescuedPrincessMessage.Action = ImpsHaveRescuedPrincessAction;


            kingJumpsOutOfCakeMessage = Events.First(e => e.Nr == 8);
            kingJumpsOutOfCakeMessage.Message = "Ihr wollt meine Dienerin entführen? Niemand klaut mir meine Bäckerin!";
            kingJumpsOutOfCakeMessage.Action = KingJumpsOutOfCakeAction;
        }

        private void KingJumpsOutOfCakeAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_08);
        }

        private void ImpsHaveRescuedPrincessAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_07);
        }

        private void ImpsAreAtSuspensionBridgeAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_06);
        }

        private void KnightEatingCakeAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_05);
        }

        private void CakeReadyAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl6_04);
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