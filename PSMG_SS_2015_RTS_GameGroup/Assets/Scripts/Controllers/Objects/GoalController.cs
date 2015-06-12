using System.Collections.Generic;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class GoalController : MonoBehaviour {

        private List<IGoalControllerListener> listeners;

        public interface IGoalControllerListener
        {
            void OnGoalReachedByImp();
        }

        public void Awake()
        {
            listeners = new List<IGoalControllerListener>();
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            string tag = collider.gameObject.tag;

            if (tag == TagReferences.Imp)
            {
                foreach (IGoalControllerListener listener in listeners)
                {
                    listener.OnGoalReachedByImp();
                }
            }

        }

        public void RegisterListener(IGoalControllerListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterLIstener(IGoalControllerListener listener)
        {
            listeners.Remove(listener);
        }

    }
}
