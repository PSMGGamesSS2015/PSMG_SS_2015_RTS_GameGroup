using System.Collections;
using System.Collections.Generic;
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

        public List<GameObject> SetOnFire(GameObject target)
        {
            return SpecialEffectsManager.Instance.SpawnFire(target.transform.position, SortingLayerReferences.MiddleForeground);
        }

        public List<GameObject> SetOnFire(GameObject target, int nrOfFlameTongues)
        {
            return SpecialEffectsManager.Instance.SpawnFire(target.transform.position, SortingLayerReferences.MiddleForeground, nrOfFlameTongues);
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

            var fire = SetOnFire(bowl.gameObject);

            yield return new WaitForSeconds(1.5f);

            fire.ForEach(f => f.GetComponent<FireParticleSystemController>().Extinguish());

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpMovementService>().Walk();

            bowl.Heat();
        }

        public void LightFurnace(FurnaceController furnace)
        {
            if (furnace.IsLight) return;
            StartCoroutine(LightingFurnaceRoutine(furnace));
        }

        private IEnumerator LightingFurnaceRoutine(FurnaceController furnace)
        {
            furnace.IsLight = true;

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSettingObjectOnFire);
            GetComponent<ImpMovementService>().Stand();

            yield return new WaitForSeconds(2.5f);

            furnace.Light();

            yield return new WaitForSeconds(1.5f);

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpMovementService>().Walk();
        }
    }
}