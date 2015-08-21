using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;

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


        protected override void RegisterEvents()
        {
            /*
        *  Servus Jan: In der folgenden auskommentierten Zeile siehst du, wie du die Sounds
        *  für die Sprechtexte des Erzählers abspielst. 
        */
            // narrator.Play(SoundReferences.FÜGEHIERDEINENNAMENEIN);   


            mapStartetMessage = events.First(e => e.Nr == 1);
            mapStartetMessage.Message = "Macht euch auf die Socken! Die Prinzessin wurde entführt.";
            mapStartetMessage.Action = MapStartetAction;    


            shieldCarrierBlockingMessage = events.First(e => e.Nr == 2);
            shieldCarrierBlockingMessage.Message = "Feiglinge verschanzen sich hinter ihren dicken Holzschilden. Daran kommt man nicht so leicht vorbei";
            shieldCarrierBlockingMessage.Action = ShieldCarrierBlockingAction;
           

            shieldCarrierHeavyMessage = events.First(e => e.Nr == 3);
            shieldCarrierHeavyMessage.Message = "Unsere Schilde sind wuchtig und schwer. Ob die Brücke das aushält?";
            shieldCarrierBlockingMessage.Action = shieldCarrierBlockingAction;


            laddersNeededMessage = events.First(e => e.Nr == 4);
            laddersNeededMessage.Message = "Legt Leitern über die Abgründe, um auf die andere Seite zu kommen.";
            laddersNeededMessage.Action = laddersNeededAction;
            

            laddersCollectedMessage = events.First(e => e.Nr == 5);
            laddersCollectedMessage.Message = "Wir haben einen Haufen Leitern gefunden.";
            laddersCollectedMessage.Action = LaddersCollectedAction;


            bombNeededMessage = events.First(e => e.Nr == 6);
            bombNeededMessage.Message = "Der Fels ist brüchig. Mit einer Bombe können wir in wegsprengen.";
            bombNeededMessage.Action = bombNeededAction;


            weaponsCollectedMessage = events.First(e => e.Nr == 7);
            weaponsCollectedMessage.Message = "Pieks pieks, bumm bumm.";
            weaponsCollectedMessage.Action = WeaponsCollectedAction;


            trollMetMessage = events.First(e => e.Nr == 8);
            trollMetMessage.Message = "Ein Troll! Vorsichtig, die sind stark und schnell sauer!";
            trollMetMessage.Action = trollMetAction;
        }

        private void trollMetAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_10);
        }

        private void bombNeededAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_08);
        }

        private void laddersNeededAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_06);
        }

        private void shieldCarrierBlockingAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_04);
        }

        private void MapStartetAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_01);
        }

        private void ShieldCarrierBlockingAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_02);
        }

        private void LaddersCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[2] += 2;
            ImpManager.Instance.NotifyMaxProfessions();
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_07);
        }

        private void WeaponsCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[0] += 1;
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[3] += 2;
            ImpManager.Instance.NotifyMaxProfessions();
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl1_09);
        }
    }
}