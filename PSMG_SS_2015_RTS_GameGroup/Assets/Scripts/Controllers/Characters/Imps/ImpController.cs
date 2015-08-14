using System.Collections.Generic;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.ExtensionMethods;
using Assets.Scripts.Helpers;
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

        // TODO Outsource
        public GameObject HorizontalLadderPrefab;
        public GameObject VerticalLadderMediumPrefab;
        public GameObject VerticalLadderLongPrefab;

        public void Awake()
        {
            InitServices();
        }

        private void InitServices()
        {
            gameObject.AddComponent<AudioHelper>();
            gameObject.AddComponent<ImpUIService>();
            gameObject.AddComponent<ImpMovementService>();
            gameObject.AddComponent<ImpTrainingService>();
            gameObject.AddComponent<ImpCollisionService>();
            gameObject.AddComponent<ImpInteractionLogicService>();

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

        public interface IImpControllerListener
        {
            void OnImpSelected(ImpController impController);
            void OnImpHurt(ImpController impController);
            void OnUntrain(ImpController impController);
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