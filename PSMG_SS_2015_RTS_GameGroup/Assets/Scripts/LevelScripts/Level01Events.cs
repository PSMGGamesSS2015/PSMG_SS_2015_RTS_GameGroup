using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;

namespace Assets.Scripts.LevelScripts
{
    public class Level01Events : LevelEvents
    { 

        private Event mapStartetMessage;
        private Event shieldCarrierBlockingMessage;
        private Event shieldCarrierHeavyMessage;
        private Event laddersNeededMessage;
        private Event laddersCollectedMessage;
        private Event weaponsCollectedMessage;
        private Event bombNeededMessage;
        private Event trollMetMessage;
        private Event professionAssignedMessage;
        private Event suspensionBridgesCrossed;
        private Event levelCompleted;

        protected override void RegisterEvents()
        { 
            mapStartetMessage = Events.First(e => e.Nr == 1);
            mapStartetMessage.Message = "Gebieter, Prinzessin Koboldigunde wurde entführt. König Krümelbart hat sie in sein tiefstes Verlies geworfen. Ihr müsst sie retten. Macht euch auf!!!";
            mapStartetMessage.Action = MapStartetAction;    

            shieldCarrierBlockingMessage = Events.First(e => e.Nr == 2);
            shieldCarrierBlockingMessage.Message = "Mein Herr. Ihr könnt Kobolde auswählen, indem ihr auf sie klickt oder die Tab-Taste benutzt.";
            shieldCarrierBlockingMessage.Action = ShieldCarrierBlockingAction;

            // TODO not played so far; should be played when the first imps is selected; create
            professionAssignedMessage = Events.First(e => e.Nr == 9);
            professionAssignedMessage.Message = "Herrvorragend. Nun gebt mir was zu tun!";

            // TODO should be played when first coward is assigned
            shieldCarrierHeavyMessage = Events.First(e => e.Nr == 3);
            shieldCarrierHeavyMessage.Message = "Hierhinter bin ich sicher, Meister. Nichts und niemand kommt an mir vorbei.";
            shieldCarrierHeavyMessage.Action = ShieldCarrierHeavyAction;

            // TODO created
            suspensionBridgesCrossed = Events.First(e => e.Nr == 10);
            suspensionBridgesCrossed.Message =
                "Diese Hängebrücke sieht zerbrechlich aus, Gebieter. Ich weiß nicht, ob sie das Gewicht unserer Schilde trägt.";

            laddersNeededMessage = Events.First(e => e.Nr == 4);
            laddersNeededMessage.Message = "Mein Herr, eure Kobolde brauchen Leitern, um Abgründe zu überqueren.";
            laddersNeededMessage.Action = LaddersNeededAction;

            laddersCollectedMessage = Events.First(e => e.Nr == 5);
            laddersCollectedMessage.Message = "Meisterlich, Meister. Wir können die Leitern nutzen, um Hindernisse zu überqueren.";
            laddersCollectedMessage.Action = LaddersCollectedAction;

            bombNeededMessage = Events.First(e => e.Nr == 6);
            bombNeededMessage.Message = "Ein dicker, fetter Brocken. Sprengt ihn, mein Herr. In unserer Waffenkammer liegen Bomben und Speere.";
            bombNeededMessage.Action = BombNeededAction;

            weaponsCollectedMessage = Events.First(e => e.Nr == 7);
            weaponsCollectedMessage.Message = "Sprengstoff und Speere. Nun sind wir gerüstet. Reißt alles nieder! Fels und Mauer, Tor und Turm.";
            weaponsCollectedMessage.Action = WeaponsCollectedAction;

            trollMetMessage = Events.First(e => e.Nr == 8);
            trollMetMessage.Message = "Ein Troll … Achtsam, mein Herr. Diese Ungeheuer sind stark und rasch verärgert. Lasst uns aus der Deckung heraus angreifen, um das Biest auszuschalten";
            trollMetMessage.Action = TrollMetAction;

            levelCompleted = Events.First(e => e.Nr == 11);
            levelCompleted.Message = "Hmmm, auf dem Weg liegen Kuchenkrümel herum. Die Prinzessin muss hier vorbeigekommen sein. Ihr habt die Spur des arglistigen Entführers gefunden. Ein Meisterstück, eure Boshaftigkeit.";
            levelCompleted.Action = LevelCompletedAction;
        }

        private void LevelCompletedAction()
        {
            // TODO
        }

        private void TrollMetAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_10);
        }

        private void BombNeededAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_08);
        }

        private void LaddersNeededAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_06);
        }

        public void ShieldCarrierHeavyAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_04);
        }

        private void MapStartetAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_01);
        }

        private void ShieldCarrierBlockingAction()
        {
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_02);
        }

        private void LaddersCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[2] += 2;
            ImpManager.Instance.NotifyMaxProfessions();
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_07);
        }

        private void WeaponsCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[0] += 1;
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[3] += 2;
            ImpManager.Instance.NotifyMaxProfessions();
            SoundManager.Instance.Narrator.PlayAfterCurrent(SoundReferences.SoundLvl1_09);
        }
    }
}