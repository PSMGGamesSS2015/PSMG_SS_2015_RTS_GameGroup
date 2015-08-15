using System.Collections;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpSchwarzeneggerService : ImpProfessionService
    {
        public bool IsAtThrowingPosition { get; set; }

        public bool IsThrowing { get; private set; }

        public ImpController CurrentProjectile { get; private set; }

        public void Awake()
        {
            InitAttributes();
            GetComponent<ImpAnimationHelper>().SwapSprites();
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
            CurrentProjectile = projectile;
            IsThrowing = true;

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSchwarzeneggerThrowing);

            yield return new WaitForSeconds(1f);

            projectile.GetComponent<ImpMovementService>().GetThrown();

            yield return new WaitForSeconds(2f);
            
            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpStanding);


            CurrentProjectile = null;
            IsThrowing = false;
        }
    }
}