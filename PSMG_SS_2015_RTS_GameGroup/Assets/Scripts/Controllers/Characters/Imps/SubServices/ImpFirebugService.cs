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
            SpecialEffectsManager.Instance.SpawnFire(target.transform.position);
        }

        public void SetOnFire(GameObject target, int nrOfFlameTongues)
        {
            SpecialEffectsManager.Instance.SpawnFire(target.transform.position, nrOfFlameTongues);
        }
    }
}