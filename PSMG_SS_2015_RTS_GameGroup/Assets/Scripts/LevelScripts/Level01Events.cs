﻿using System.Linq;
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


        protected override void RegisterEvents()
        {
            var narrator = SoundManager.Instance.Narrator;

            mapStartetMessage = events.First(e => e.Nr == 1);

            mapStartetMessage.Message = "Macht euch auf die Socken! Die Prinzessin wurde entführt.";
            /*
             *  Servus Jan: In der folgenden auskommentierten Zeile siehst du, wie du die Sounds
             *  für die Sprechtexte des Erzählers abspielst. 
             */
            // narrator.Play(SoundReferences.FÜGEHIERDEINENNAMENEIN);

            //narrator.Play(SoundReferences.SoundLvl101); SoundReferences nicht gefunden


            shieldCarrierBlockingMessage = events.First(e => e.Nr == 2);

            shieldCarrierBlockingMessage.Message = "Feiglinge verschanzen sich hinter ihren dicken Holzschilden. Daran kommt man nicht so leicht vorbei";

            shieldCarrierHeavyMessage = events.First(e => e.Nr == 3);

            shieldCarrierHeavyMessage.Message = "Unsere Schilde sind wuchtig und schwer. Ob die Brücke das aushält?";

            laddersNeededMessage = events.First(e => e.Nr == 4);

            laddersNeededMessage.Message = "Legt Leitern über die Abgründe, um auf die andere Seite zu kommen.";

            laddersCollectedMessage = events.First(e => e.Nr == 5);

            laddersCollectedMessage.Message = "Wir haben einen Haufen Leitern gefunden.";

            laddersCollectedMessage.Action = LaddersCollectedAction;

            bombNeededMessage = events.First(e => e.Nr == 6);

            bombNeededMessage.Message = "Der Fels ist brüchig. Mit einer Bombe können wir in wegsprengen.";

            weaponsCollectedMessage = events.First(e => e.Nr == 7);

            weaponsCollectedMessage.Message = "Pieks pieks, bumm bumm.";

            weaponsCollectedMessage.Action = WeaponsCollectedAction;

            trollMetMessage = events.First(e => e.Nr == 8);

            trollMetMessage.Message = "Ein Troll! Vorsichtig, die sind stark und schnell sauer!";
        }

        private void LaddersCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[2] += 2;
            ImpManager.Instance.NotifyMaxProfessions();
        }

        private void WeaponsCollectedAction()
        {
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[0] += 1;
            LevelManager.Instance.CurrentLevel.CurrentLevelConfig.MaxProfessions[3] += 2;
            ImpManager.Instance.NotifyMaxProfessions();
        }
    }
}