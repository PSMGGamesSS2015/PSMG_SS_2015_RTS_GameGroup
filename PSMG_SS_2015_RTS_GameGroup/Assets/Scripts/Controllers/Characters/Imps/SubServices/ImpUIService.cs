using Assets.Scripts.AssetReferences;
using Assets.Scripts.LevelScripts;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpUIService : MonoBehaviour
    {
        public ImpSelection Selection { get; private set; }

        public void Awake()
        {
            Selection = GetComponentInChildren<ImpSelection>();
        }

        public void OnMouseDown()
        {
            
            var impController = GetComponent<ImpController>();
            impController.Listeners.ForEach(l => l.OnImpSelected(impController));

            CheckIfFirstImpInFirstLevelIsClicked();
        }

        private static void CheckIfFirstImpInFirstLevelIsClicked()
        {
            if (LevelManager.Instance.CurrentLevel.CurrentLevelConfig.Name != SceneReferences.Level01Koboldingen) return;
            
            var events = (Level01Events) LevelManager.Instance.CurrentLevelEvents;

            if (!events.ImpIsClickable) return;
            if (events.ImpHasBeenClicked) return;

            events.AssignProfessionMessage.TriggerManually();
        }
    }
}