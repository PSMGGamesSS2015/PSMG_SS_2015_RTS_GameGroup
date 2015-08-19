using System.Collections;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpPwnedService : MonoBehaviour
    {
        public enum PwningType
        {
            Scorching
        }

        public void Pwn(PwningType pwningType)
        {
            switch (pwningType)
            {
                case PwningType.Scorching:
                    Scorch();
                    break;
            }
        }

        private void Scorch()
        {
            StartCoroutine(ScorchingRoutine());
        }

        private IEnumerator ScorchingRoutine()
        {
            GetComponent<ImpAnimationHelper>().ImpInventory.HideItems();
            GetComponent<ImpTrainingService>().IsTrainable = false;

            var spawnPosition = new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y,
                gameObject.transform.position.z);
            var fire = SpecialEffectsManager.Instance.SpawnFire(spawnPosition,
                SortingLayerReferences.MiddleForeground);
            fire.ForEach(f => f.transform.parent = gameObject.transform);

            GetComponent<ImpSpriteManagerService>().Sprites.ToList().ForEach(s => s.color = Color.black);

            GetComponent<ImpMovementService>().Run();

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpWalkingBomb);

            yield return new WaitForSeconds(1f);

            GetComponent<ImpCollisionService>().CircleCollider2D.enabled = false;

            yield return new WaitForSeconds(1f);

            GetComponent<ImpController>().LeaveGame();
        }
    }
}