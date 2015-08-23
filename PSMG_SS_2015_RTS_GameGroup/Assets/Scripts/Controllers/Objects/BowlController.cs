using System.Collections;
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

        public bool IsBeingBattered { get; set; }

        public bool HasBeenBattered { get; private set; }

        public bool HasBeenHeated { get; private set; }
        public bool IsBeingHeated { get; set; }

        public void Awake()
        {
            HasFlourBeenAdded = false;
            HasBeenBattered = false;
            HasBeenHeated = false;
            IsBeingBattered = false;
            IsBeingHeated = false;

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
            if (!HasFlourBeenAdded || HasBeenBattered || HasBeenHeated || IsBeingBattered) return;

            HasBeenBattered = true;

            var level06Events = (Level06Events)LevelManager.Instance.CurrentLevelEvents;
            level06Events.CakeAlmostReadyMessage.TriggerManually();
        }

        public void Heat()
        {
            if (!HasFlourBeenAdded || !HasBeenBattered || HasBeenHeated) return;

            HasBeenHeated = true;
            StartCoroutine(HeatingRoutine());
        }

        private IEnumerator HeatingRoutine()
        {
            // TODO Wait a bit

            yield return new WaitForSeconds(0f);

            // TODO replace bowl with cake

            // TODO make cake steam

            // TODO Wait a bit

            // TODO open door

            // TODO wait a bit

            // TODO make knight move to cake

            // TODO make knight eat cake

            var level06Events = (Level06Events)LevelManager.Instance.CurrentLevelEvents;
            level06Events.CakeReadyMessage.TriggerManually();
        }
    }
}