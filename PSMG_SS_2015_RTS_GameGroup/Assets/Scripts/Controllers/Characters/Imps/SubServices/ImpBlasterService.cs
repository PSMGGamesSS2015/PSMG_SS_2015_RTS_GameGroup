using System.Collections;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Helpers;
using Assets.Scripts.Managers;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpBlasterService : ImpProfessionService
    {
        private Counter bombCounter;

        private ImpTrainingService impTrainingService;
        private ImpMovementService impMovementService;
        private ImpAnimationHelper impAnimationService;
        private AudioHelper impAudioService;
        private Vector3 screenPos;
        private Vector3 screenPosOfTopMargin;
        public int yOffset = 20;

        public void Awake()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            impTrainingService = GetComponent<ImpTrainingService>();
            impMovementService = GetComponent<ImpMovementService>();
            impAnimationService = GetComponent<ImpAnimationHelper>();
            impAudioService = GetComponent<AudioHelper>();
        }

        public void Start()
        {
            SetupBombCounter();
            screenPosOfTopMargin = Camera.main.WorldToScreenPoint(LevelManager.Instance.CurrentLevel.TopMargin.transform.position);
        }

        public void Update()
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
        }

        public void OnGUI()
        {
            Debug.Log(screenPosOfTopMargin.y);
            
            GUI.Label(new Rect(screenPos.x, screenPosOfTopMargin.y - screenPos.y - 100, 100, 25), ((int) bombCounter.CurrentCount).ToString());
        }

        private void SetupBombCounter()
        {
            if (bombCounter != null) Destroy(bombCounter.gameObject);
            bombCounter = Counter.SetCounter(this.gameObject, 3f, DetonateBomb, false);
        }

        public void DetonateBomb()
        {
            StartCoroutine(DetonatingRoutine());
        }

        private IEnumerator DetonatingRoutine()
        {
            var formerMovementSpeed = impMovementService.MovementSpeed;
            var isFlippingNecessary = (formerMovementSpeed < 0);
            impMovementService.Stand();

            if (isFlippingNecessary)
            {
                impAnimationService.FlipExplosion();
            }

            impAnimationService.DisplayExplosion();
            impAnimationService.Play(AnimationReferences.ImpDetonatingBomb);
            impAudioService.Play(SoundReferences.BombExplosion);

            yield return new WaitForSeconds(1f);

            if (isFlippingNecessary)
            {
                impAnimationService.FlipExplosion();
            }

            var objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 2f);

            foreach (var c in objectsWithinRadius.Where(c => c.gameObject.tag == TagReferences.RockyArc))
            {
                c.GetComponent<RockyArcScript>().Detonate();
            }

            foreach (var c in objectsWithinRadius.Where(c => c.gameObject.tag == TagReferences.FragileRock))
            {
                c.GetComponent<SpriteRenderer>().enabled = false;
                Destroy(c);
            }

            impMovementService.Walk();
            impTrainingService.IsTrainable = true;
            impTrainingService.Untrain();
        }

    }
}