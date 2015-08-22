using System.Collections;
using Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices;
using Assets.Scripts.Helpers;
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
            gameObject.AddComponent<AudioHelper>();
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
                // TODO Highlight being wounded
            }
            
        }

        private IEnumerator LeavingRoutine()
        {
            yield return new WaitForSeconds(0f);
        }

        public void LeaveGame()
        {
            Destroy(gameObject);
        }
    }
}
