using System.Linq;
using Assets.Scripts.LevelScripts;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class BowlController : MonoBehaviour
    {
        private SpriteRenderer flour;
        private const string FlourName = "Flour";

        public bool HasFlourBeenAdded { get; private set; }

        public bool HasBeenBattered { get; private set; }

        public bool HasBeenHeated { get; private set; }

        public void Awake()
        {
            HasFlourBeenAdded = false;
            HasBeenBattered = false;
            HasBeenHeated = false;

            flour = GetComponentsInChildren<SpriteRenderer>().First(sr => sr.name == FlourName);
        }

        public void AddFlour()
        {
            if (HasFlourBeenAdded || HasBeenBattered || HasBeenHeated) return;

            HasFlourBeenAdded = true;
            flour.enabled = true;

            var level06Events = (Level06Events) LevelManager.Instance.CurrentLevelEvents;
            level06Events.FlourHasFallenIntoBowlMessage.TriggerManually();
        }

        public void BatterDough()
        {
            if (!HasFlourBeenAdded || HasBeenBattered || HasBeenHeated) return;

            HasBeenBattered = true;
            // TODO
        }

        public void Heat()
        {
            if (!HasFlourBeenAdded || !HasBeenBattered || HasBeenHeated) return;

            HasBeenHeated = true;
            // TODO 
        }
    }
}