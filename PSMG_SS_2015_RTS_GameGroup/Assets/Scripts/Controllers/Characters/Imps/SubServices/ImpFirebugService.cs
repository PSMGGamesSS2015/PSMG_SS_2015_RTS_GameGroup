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

            StartCoroutine(LightGaslightRoutine(gaslight));
        }

        private IEnumerator LightGaslightRoutine(GaslightController gaslight)
        {
            GetComponent<ImpTrainingService>().IsTrainable = false;

            GetComponent<ImpMovementService>().Stand();
            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSettingObjectOnFire);

            yield return new WaitForSeconds(4f);

            gaslight.Light();

            GetComponent<ImpMovementService>().Walk();
            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();

            GetComponent<ImpTrainingService>().IsTrainable = true;
        }

        public List<GameObject> SetOnFire(GameObject target)
        {
            return SpecialEffectsManager.Instance.SpawnFire(target.transform.position, SortingLayerReferences.MiddleForeground);
        }

        public List<GameObject> SetOnFire(GameObject target, string sortingLayer, int positionInLayer)
        {
            return SpecialEffectsManager.Instance.SpawnFire(target.transform.position, sortingLayer, positionInLayer);
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
            GetComponent<ImpTrainingService>().IsTrainable = false;
            bowl.IsBeingHeated = true;

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSettingObjectOnFire);
            GetComponent<ImpMovementService>().Stand();

            yield return new WaitForSeconds(2.5f);

            var fire = SetOnFire(bowl.gameObject, SortingLayerReferences.MiddleForeground, 35);

            yield return new WaitForSeconds(1.5f);

            fire.ForEach(f => f.GetComponent<FireParticleSystemController>().Extinguish());

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpMovementService>().Walk();

            bowl.Heat();
            GetComponent<ImpTrainingService>().IsTrainable = true;
        }

        public void LightFurnace(FurnaceController furnace)
        {
            if (furnace.IsLight) return;
            StartCoroutine(LightingFurnaceRoutine(furnace));
        }

        private IEnumerator LightingFurnaceRoutine(FurnaceController furnace)
        {
            GetComponent<ImpTrainingService>().IsTrainable = false;
            furnace.IsLight = true;

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSettingObjectOnFire);
            GetComponent<ImpMovementService>().Stand();

            yield return new WaitForSeconds(2.5f);

            furnace.Light();

            yield return new WaitForSeconds(1.5f);

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpMovementService>().Walk();
            GetComponent<ImpTrainingService>().IsTrainable = true;
        }

        public void FireCanon(CanonController canon)
        {
            if (canon.IsBeingFired) return;

            canon.IsBeingFired = true;
            StartCoroutine(FiringCanonRoutine(canon));
        }

        private IEnumerator FiringCanonRoutine(CanonController canon)
        {
            GetComponent<ImpTrainingService>().IsTrainable = false;
            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpSettingObjectOnFire);
            GetComponent<ImpMovementService>().Stand();

            yield return new WaitForSeconds(1.5f);

            canon.Light();

            GetComponent<ImpAnimationHelper>().PlayWalkingAnimation();
            GetComponent<ImpMovementService>().Walk();

            yield return new WaitForSeconds(1.5f);

            canon.Fire();

            canon.IsBeingFired = false;
            GetComponent<ImpTrainingService>().IsTrainable = true;
        }
    }
}