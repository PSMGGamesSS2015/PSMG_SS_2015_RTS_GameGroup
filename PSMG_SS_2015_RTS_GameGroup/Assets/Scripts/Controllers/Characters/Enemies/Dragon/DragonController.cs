using System.Collections;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon
{
    public class DragonController : EnemyController
    {

        public bool IsWounded { get; private set; }

        public void Awake()
        {
            IsLeaving = false;
            IsWounded = false;

            InitServices();
        }

        public bool IsLeaving { get; set; }

        private void InitServices()
        {
            gameObject.AddComponent<DragonMovementService>();
            gameObject.AddComponent<DragonSteamBreathingService>();
            gameObject.AddComponent<DragonAudioService>();
            gameObject.AddComponent<DragonCollisionService>();
            gameObject.AddComponent<DragonSpriteManagerService>();
        }


        public void ReceiveHit()
        {
            if (IsWounded)
            {
                StartCoroutine(LeavingRoutine());
            }
            else
            {
                IsWounded = true;
                GetComponent<DragonAudioService>().Voice.Play(SoundReferences.DragonCry);
                GetComponent<DragonSpriteManagerService>().Blink();
            }
            
        }

        private IEnumerator LeavingRoutine()
        {
            GetComponent<DragonMovementService>().Stand();
            GetComponent<DragonAudioService>().Voice.Play(SoundReferences.DragonDeath);

            yield return new WaitForSeconds(1.5f);

            LeaveGame();
        }

        public void LeaveGame()
        {
            Destroy(gameObject);
        }
    }
}
