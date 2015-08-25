using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices;
using Assets.Scripts.LevelScripts;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.UIManagerAndServices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class BowlController : MonoBehaviour
    {
        private SpriteRenderer flour;
        private List<SpriteRenderer> spriteRenderers; 
        private const string FlourName = "Flour";
        private const string ColorOfBatteredDoughHexString = "D49A6A";

        public GameObject TastyTartPrefab;

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

            spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();

            flour = GetComponentsInChildren<SpriteRenderer>().First(sr => sr.name == FlourName);
        }

        public void AddFlour()
        {
            if (HasFlourBeenAdded || HasBeenBattered || HasBeenHeated) return;

            HasFlourBeenAdded = true;

            StartCoroutine(AddingFlourRoutine());
        }

        private IEnumerator AddingFlourRoutine()
        {
            yield return new WaitForSeconds(6f);

            flour.enabled = true;

            var level06Events = (Level06Events)LevelManager.Instance.CurrentLevelEvents;
            level06Events.FlourHasFallenIntoBowlMessage.TriggerManually();
        }

        public void BatterDough()
        {
            if (!HasFlourBeenAdded || HasBeenBattered || HasBeenHeated) return;

            HasBeenBattered = true;
            Color colorOfBatteredDough;
            Color.TryParseHexString(ColorOfBatteredDoughHexString, out colorOfBatteredDough);
            flour.color = colorOfBatteredDough;

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
            yield return new WaitForSeconds(1f);

            var position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, gameObject.transform.position.z);
            Instantiate(TastyTartPrefab, position, Quaternion.identity);

            spriteRenderers.ForEach(sr => sr.enabled = false);
            GetComponent<Collider2D>().enabled = false;

            yield return new WaitForSeconds(2f);

            var level06Events = (Level06Events)LevelManager.Instance.CurrentLevelEvents;

            level06Events.DoorToOvens.Open();
            
            level06Events.CakeReadyMessage.TriggerManually(Speaker.Knight);

            yield return new WaitForSeconds(2f);

            level06Events.KnightBehindFirstGate.GetComponent<KnightMovementService>().Walk();
            level06Events.KnightBehindFirstGate.GetComponent<KnightAnimationHelper>().Play(AnimationReferences.KnightWalking);
        }
    }
}