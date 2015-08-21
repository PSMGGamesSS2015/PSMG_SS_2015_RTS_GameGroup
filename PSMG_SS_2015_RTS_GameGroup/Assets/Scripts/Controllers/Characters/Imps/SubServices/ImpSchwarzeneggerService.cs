using System.Collections;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpSchwarzeneggerService : ImpProfessionService
    {
        public bool IsAtThrowingPosition { get; set; }

        public bool IsThrowing { get; private set; }

        public ImpController CurrentProjectile { get; private set; }

        private GameObject rightHand;

        public void Awake()
        {
            InitAttributes();
            GetComponent<ImpSpriteManagerService>().SwapSprites();

            rightHand = GetComponentsInChildren<SpriteRenderer>()
                .ToList()
                .First(sr => sr.gameObject.name == "RightPalm").transform.parent.gameObject;
        }

        private void InitAttributes()
        {
            IsAtThrowingPosition = false;
            IsThrowing = false;
        }

        public void ThrowImp(ImpController projectile)
        {
            StartCoroutine(ThrowingImpRoutine(projectile));
        }

        private IEnumerator ThrowingImpRoutine(ImpController projectile)
        {

            HandleCollisionManagementWithProjectile(projectile);

            CurrentProjectile = projectile;

            IsThrowing = true;
            
            TakeProjectileInHand(projectile);

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSchwarzeneggerThrowing);

            yield return new WaitForSeconds(1f);

            ThrowProjectile(projectile);

            yield return new WaitForSeconds(2f);

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpStanding);

            ResetProjectileToWalkingPosition(projectile);

            CurrentProjectile = null;
            IsThrowing = false;
        }

        private void HandleCollisionManagementWithProjectile(ImpController projectile)
        {
            Physics2D.IgnoreCollision(GetComponent<ImpCollisionService>().CircleCollider2D,
                projectile.GetComponent<ImpCollisionService>().CircleCollider2D, true);
        }

        private static void ResetProjectileToWalkingPosition(ImpController projectile)
        {
            projectile.GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            projectile.gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }

        private static void ThrowProjectile(ImpController projectile)
        {
            projectile.GetComponent<ImpMovementService>().GetThrown();
            projectile.transform.parent = null;
        }

        private void TakeProjectileInHand(ImpController projectile)
        {
            projectile.transform.parent = rightHand.transform;
            projectile.gameObject.transform.position = new Vector3(projectile.gameObject.transform.position.x + 0.35f,
                projectile.gameObject.transform.position.y, projectile.gameObject.transform.position.z);
            projectile.GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpStanding);
        }
    }
}