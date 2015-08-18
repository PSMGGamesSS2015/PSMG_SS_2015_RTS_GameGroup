using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public abstract class LevelEvents : MonoBehaviour
    {
        protected List<Event> events;

        public void Awake()
        {
            events = new List<Event>();

            GameObject.FindGameObjectsWithTag(TagReferences.Event)
                .ToList()
                .ForEach(e => events.Add(e.GetComponent<Event>()));
        }

        public void Start()
        {
            RegisterEvents();
        }

        protected abstract void RegisterEvents();
    }
}