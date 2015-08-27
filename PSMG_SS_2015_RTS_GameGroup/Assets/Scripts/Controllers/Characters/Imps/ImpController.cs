using System.Collections.Generic;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.ExtensionMethods;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    /// <summary>
    ///     The ImpController is a component attached to every instance of
    ///     an Imp prefab. It manages movement patterns and collision detection
    ///     of imps and listens for click events on the imps.
    /// </summary>
    public class ImpController : MonoBehaviour
    {

        public GameObject HorizontalLadderPrefab;
        public GameObject VerticalLadderMediumPrefab;
        public GameObject VerticalLadderLongPrefab;

        public void Awake()
        {
            IsLeaving = false;

            InitServices();
        }

        private void InitServices()
        {
            gameObject.AddComponent<ImpAudioService>();
            gameObject.AddComponent<ImpUIService>();
            gameObject.AddComponent<ImpMovementService>();
            gameObject.AddComponent<ImpTrainingService>();
            gameObject.AddComponent<ImpCollisionService>();
            gameObject.AddComponent<ImpInteractionLogicService>();
            gameObject.AddComponent<ImpPwnedService>();

            Listeners = new List<IImpControllerListener>();
        }

        public void LeaveGame()
        {
            for (var i = Listeners.Count - 1; i >= 0; i--)
            {
                Listeners[i].OnImpHurt(this);
            }
            Listeners.Clear();
            this.StopAllCounters();
            Destroy(gameObject);
        }

        # region listener interface

        public List<IImpControllerListener> Listeners { get; private set; }
        public bool IsLeaving { get; set; }

        public interface IImpControllerListener
        {
            void OnImpSelected(ImpController impController);
            void OnImpHurt(ImpController impController);
            void OnUntrain(ImpController impController);
            void OnCheckpointReached(CheckPointController checkPointController);
        }

        public void RegisterListener(IImpControllerListener listener)
        {
            Listeners.Add(listener);
        }

        public void UnregisterListener(IImpControllerListener listener)
        {
            Listeners.Remove(listener);
        }

        #endregion
    }
}