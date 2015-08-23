using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Objects;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpFirebugService : ImpProfessionService
    {
        public void LightGaslight(GaslightController gaslight)
        {
            if (gaslight.IsLight) return;
            gaslight.Light();
        }

        public void SetOnFire(GameObject target)
        {
            SpecialEffectsManager.Instance.SpawnFire(target.transform.position, SortingLayerReferences.MiddleForeground);
        }

        public void SetOnFire(GameObject target, int nrOfFlameTongues)
        {
            SpecialEffectsManager.Instance.SpawnFire(target.transform.position, SortingLayerReferences.MiddleForeground, nrOfFlameTongues);
        }

        public void HeatDough(BowlController bowl)
        {
            if (bowl.IsBeingHeated) return;

            StartCoroutine(HeatingDoughRoutine(bowl));
        }

        private IEnumerator HeatingDoughRoutine(BowlController bowl)
        {
            bowl.IsBeingHeated = true;

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSettingObjectOnFire);
            GetComponent<ImpMovementService>().Stand();

            yield return new WaitForSeconds(2.5f);

            SetOnFire(bowl.gameObject);

            yield return new WaitForSeconds(1.5f);

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpMovementService>().Walk();

            bowl.Heat();
        }
    }
}