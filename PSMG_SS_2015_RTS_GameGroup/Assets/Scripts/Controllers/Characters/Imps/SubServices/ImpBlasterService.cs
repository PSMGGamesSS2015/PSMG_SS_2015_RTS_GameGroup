using System.Collections;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
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
        }

        public void Start()
        {
            SetupBombCounter();
            screenPosOfTopMargin =
                Camera.main.WorldToScreenPoint(LevelManager.Instance.CurrentLevel.TopMargin.transform.position);
        }

        public void Update()
        {
            screenPos = Camera.main.WorldToScreenPoint(transform.position);
        }

        public void OnGUI()
        {
            GUI.Label(new Rect(screenPos.x, screenPosOfTopMargin.y - screenPos.y - 100, 100, 25),
                ((int) bombCounter.CurrentCount).ToString());
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
            GetComponent<ImpAudioService>().Sounds.Play(SoundReferences.BombExplosion);

            if (isFlippingNecessary)
            {
                impAnimationService.FlipExplosion();
            }

            HandleImpactOnObjectsInRange();

            yield return new WaitForSeconds(1f);

            impMovementService.Walk();
            impTrainingService.IsTrainable = true;
            impTrainingService.Untrain();
        }

        private void HandleImpactOnObjectsInRange()
        {
            // TODO Check if flour falls into kettle in backstuebchen

            var objectsWithinRadius = Physics2D.OverlapCircleAll(gameObject.transform.position, 4f);

            objectsWithinRadius.ToList()
                .Where(o => o.tag == TagReferences.RockyArc)
                .ToList()
                .ForEach(o => o.GetComponent<RockyArcScript>().Detonate());

            objectsWithinRadius.ToList().Where(c => c.tag == TagReferences.FragileRock).ToList().ForEach(Destroy);
            objectsWithinRadius.ToList().Where(c => c.tag == TagReferences.Explodable).ToList().ForEach(ApplyChaos);
            objectsWithinRadius.ToList().Where(c => c.tag == TagReferences.BatterBowl).ToList().ForEach(DetonateBatterBowl);
            objectsWithinRadius.ToList().Where(c => c.tag == TagReferences.FlourBag).ToList().ForEach(DetonateFlourBag);
        }

        private void DetonateBatterBowl(Collider2D collider)
        {
            // TODO add flour to bowl
        }

        private void DetonateFlourBag(Collider2D collider)
        {
            var flourBagController = collider.gameObject.GetComponent<FlourBagController>();

            flourBagController.Explode();
        }

        private void ApplyChaos(Collider2D collider)
        {
            var rb = collider.GetComponent<Rigidbody2D>();
            if (rb == null) return;

            rb.isKinematic = false;
            var x = collider.transform.position.x - gameObject.transform.position.x;
            var y = collider.transform.position.y - gameObject.transform.position.y;
            var positionRelativeToImp = new Vector2(x, y);
            rb.velocity = new Vector2(positionRelativeToImp.x*15, positionRelativeToImp.y*15);
        }
    }
}