using System.Collections;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonFireBreathingService : MonoBehaviour
    {
        public GameObject DragonFirePrefab;

        public void Awake()
        {
            IsBreathingFire = false;
            IsFirstBreath = true;
            BreathingCounter = 0;
        }

        public void BreathFire()
        {
            if (IsBreathingFire) return;

            if (IsFirstBreath) IsFirstBreath = false;

            BreathingCounter = 0;
            StartCoroutine(FireBreathingRoutine());
        }

        public bool IsBreathingFire { get; set; }
        public bool IsFirstBreath { get; private set; }
        public int BreathingCounter { get; set; }

        private IEnumerator FireBreathingRoutine()
        {
            IsBreathingFire = true;

            GetComponent<DragonAnimationHelper>().PlayBreathingAnimation();
            GetComponent<DragonMovementService>().StayInPosition();
            GetComponent<DragonSpriteManagerService>().HighlightNostrilsInColor(Color.red);

            yield return new WaitForSeconds(2.0f);

            InstantiateDragonFire();
            GetComponent<DragonAudioService>().Voice.Play(SoundReferences.DragonBurr);

            yield return new WaitForSeconds(1.0f);

            GetComponent<DragonAnimationHelper>().PlayFlyingAnimation();
            GetComponent<DragonMovementService>().Flydown();
            GetComponent<DragonSpriteManagerService>().HighlightNostrilsInDefaultColor();

            IsBreathingFire = false;
        }

        private void InstantiateDragonFire()
        {
            var posMiddle = gameObject.transform.position;
            var posMiddleLeft = new Vector3(gameObject.transform.position.x -1, gameObject.transform.position.y, gameObject.transform.position.z);
            var posOuterLeft = new Vector3(gameObject.transform.position.x - 2, gameObject.transform.position.y, gameObject.transform.position.z);
            var posMiddleRight = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, gameObject.transform.position.z);
            var posOuterRight = new Vector3(gameObject.transform.position.x + 2, gameObject.transform.position.y, gameObject.transform.position.z);

            Instantiate(DragonFirePrefab, posMiddle, Quaternion.identity);
            Instantiate(DragonFirePrefab, posMiddleLeft, Quaternion.identity);
            Instantiate(DragonFirePrefab, posOuterLeft, Quaternion.identity);
            Instantiate(DragonFirePrefab, posMiddleRight, Quaternion.identity);
            Instantiate(DragonFirePrefab, posOuterRight, Quaternion.identity);
        }
    }
}