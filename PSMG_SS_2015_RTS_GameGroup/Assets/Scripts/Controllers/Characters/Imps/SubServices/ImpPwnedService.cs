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
            Scorching,
            Smashing
        }

        public void Pwn(PwningType pwningType)
        {
            switch (pwningType)
            {
                case PwningType.Scorching:
                    Scorch();
                    break;
                case PwningType.Smashing:
                    Smash();
                    break;
            }
        }

        private void Smash()
        {
            if (GetComponent<ImpController>().IsLeaving) return;
            GetComponent<ImpController>().IsLeaving = true;

            StartCoroutine(SmashingRoutine());
        }

        private IEnumerator SmashingRoutine()
        {
            GetComponent<ImpAnimationHelper>().ImpInventory.HideItems();
            GetComponent<ImpTrainingService>().IsTrainable = false;
            GetComponent<ImpMovementService>().Stand();

            GetComponent<ImpAnimationHelper>().Play(AnimationReferences.ImpDead);
            GetComponent<ImpAudioService>().Voice.PlayAsLast(SoundReferences.ImpDamage);

            yield return new WaitForSeconds(1f);

            GetComponent<ImpController>().LeaveGame();
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

            GetComponent<ImpAudioService>().Voice.PlayAsLast(SoundReferences.ImpDamage);

            yield return new WaitForSeconds(1f);

            GetComponent<ImpCollisionService>().CircleCollider2D.enabled = false;

            yield return new WaitForSeconds(1f);

            GetComponent<ImpController>().LeaveGame();
        }
    }
}