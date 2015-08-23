using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.LevelScripts
{
    public class Level06Events : LevelEvents
    {
        public Event LevelStartedMessage;
        public Event FlourHasFallenIntoBowlMessage;
        public Event CakeAlmostReadyMessage;
        public Event CakeReadyMessage;
        public Event KnightEatingCakeMessage;
        public Event ImpsAreAtEndOfFirstFloorMessage;
        public Event ImpsHaveRescuedPrincessMessage;
        public Event KingJumpsOutOfCakeMessage;

        protected override void RegisterEvents()
        {
            LevelStartedMessage = Events.First(e => e.Nr == 1);
            LevelStartedMessage.Message = "Meister, sprengt dieses lästige Tor aus dem Weg.";
            LevelStartedMessage.Action = LevelStartedAction;

            FlourHasFallenIntoBowlMessage = Events.First(e => e.Nr == 2);
            FlourHasFallenIntoBowlMessage.Message = "Mmh, es ist Mehl in die Schüssel gefallen. Mit Wasser und etwas zum Rühren könnten wir uns einen Kuchen backen.";
            FlourHasFallenIntoBowlMessage.Action = FlourHasFallenIntoBowlAction;


            CakeAlmostReadyMessage = Events.First(e => e.Nr == 3);
            CakeAlmostReadyMessage.Message = "Das riecht fast so gut wie die Leckereien der Prinzessin, mein Herr. Er muss nur noch kurz backen.";
            CakeAlmostReadyMessage.Action = CakeAlmostReadyAction;


            CakeReadyMessage = Events.First(e => e.Nr == 4);
            CakeReadyMessage.Message = "Mmmmmhh.";
            CakeReadyMessage.Action = CakeReadyAction;


            KnightEatingCakeMessage = Events.First(e => e.Nr == 5);
            KnightEatingCakeMessage.Message = "Kuchen … hmm … nur ein kleines Stück.";
            // TODO knightEatingCakeMessage.Message = "Lasst uns schnell weitergehen, bevor der Ritter aufgegessen hat.";
            KnightEatingCakeMessage.Action = KnightEatingCakeAction;


            ImpsAreAtEndOfFirstFloorMessage = Events.First(e => e.Nr == 6);
            ImpsAreAtEndOfFirstFloorMessage.Message = "Wo ist bloß die Prinzessin? Wir konnten sie nirgendwo finden. Dringt tiefer ins Verlies vor. Versucht, diese Brücke zu Fall zu bringen.";
            ImpsAreAtEndOfFirstFloorMessage.Action = ImpsAreAtSuspensionBridgeAction;


            ImpsHaveRescuedPrincessMessage = Events.First(e => e.Nr == 7);
            ImpsHaveRescuedPrincessMessage.Message = "Geschafft, geschafft, geschafft! Nun bringt die Prinzessin sicher aus dem Verlies.";
            ImpsHaveRescuedPrincessMessage.Action = ImpsHaveRescuedPrincessAction;


            KingJumpsOutOfCakeMessage = Events.First(e => e.Nr == 8);
            KingJumpsOutOfCakeMessage.Message = "Ihr wollt meine Dienerin entführen? Niemand klaut mir meine Bäckerin!";
            KingJumpsOutOfCakeMessage.Action = KingJumpsOutOfCakeAction;
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

            // TODO Open Door and make knight move in
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