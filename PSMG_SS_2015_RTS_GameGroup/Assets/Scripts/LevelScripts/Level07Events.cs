using System.Linq;
using Assets.Scripts.Managers;
using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.LevelScripts
{
    public class Level07Events : LevelEvents
    {
        private Event impsReachedVillageMessage;

        protected override void RegisterEvents()
        {
            // TODO
            impsReachedVillageMessage = events.First(e => e.Nr == 1);
            impsReachedVillageMessage.Message = "Wir sind wieder zu Hause, mein Herr. Ein Hoch auf den Meister! Ein Hoch auf den Gebieter! Ein Hoch auf unseren Imperator! Nun lasst uns essen und feiern.";
            impsReachedVillageMessage.Action = impsReachedVillageAction;
        }

        private void impsReachedVillageAction()
        {
            SoundManager.Instance.Narrator.Play(SoundReferences.SoundLvl7_01);
        }
    }
}