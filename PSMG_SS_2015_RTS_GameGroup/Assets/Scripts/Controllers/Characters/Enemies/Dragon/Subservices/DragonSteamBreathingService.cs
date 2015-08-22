using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonSteamBreathingService : MonoBehaviour, TriggerCollider2D.ITriggerCollider2DListener
    {

        private TriggerCollider2D steamBreathingRange;
        public List<ImpController> ImpsInBreathingRange { get; private set; }
        private ParticleSystem steamParticleSystem;

        public void Awake()
        {
            steamBreathingRange =
                GetComponentsInChildren<TriggerCollider2D>()
                    .First(tc => tc.gameObject.tag == TagReferences.DragonSteamBreathingRange);
            
            steamBreathingRange.RegisterListener(this);

            steamParticleSystem =
                GetComponentsInChildren<ParticleSystem>().First(ps => ps.tag == TagReferences.DragonSteamBreath);

            ImpsInBreathingRange = new List<ImpController>();
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerEnter2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != steamBreathingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.ImpCollisionCheck:
                    OnTriggerEnterImp(collider.GetComponentInParent<ImpController>());
                    break;
            }
        }

        private void OnTriggerEnterImp(ImpController imp)
        {
            if (!ImpsInBreathingRange.Contains(imp))
                ImpsInBreathingRange.Add(imp);
        }

        public void BreathSteam()
        {
            StartCoroutine(SteamBreathingRoutine());
        }

        private IEnumerator SteamBreathingRoutine()
        {
            GetComponent<DragonAnimationHelper>().PlayBreathingAnimation();
            GetComponent<DragonMovementService>().StayInPosition();
            

            yield return new WaitForSeconds(2.15f);

            steamParticleSystem.Play();
            GetComponent<DragonAudioService>().Voice.Play(SoundReferences.DragonWoahh);

            yield return new WaitForSeconds(0.2f);

            GetComponent<DragonMovementService>().ChangeDirection();
            ImpsInBreathingRange.ForEach(BounceBack);

            yield return new WaitForSeconds(0.65f);

            GetComponent<DragonAnimationHelper>().PlayFlyingAnimation();
            steamParticleSystem.Stop();
            GetComponent<DragonMovementService>().Run();

        }

        private void BounceBack(ImpController imp)
        {
            if (IsImpRightOfDragon(imp))
            {
                imp.GetComponent<ImpMovementService>().GetBouncedBack(true);
            }
            else
            {
                imp.GetComponent<ImpMovementService>().GetBouncedBack(false);
            }
        }

        private bool IsImpRightOfDragon(ImpController imp)
        {
            return imp.gameObject.transform.position.x > gameObject.transform.position.x;
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerExit2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != steamBreathingRange.GetInstanceID()) return;

            switch (collider.gameObject.tag)
            {
                case TagReferences.Imp:
                    OnTriggerExitImp(collider.GetComponentInParent<ImpController>());
                    break;
            }
        }

        void TriggerCollider2D.ITriggerCollider2DListener.OnTriggerStay2D(TriggerCollider2D self, Collider2D collider)
        {
            if (self.GetInstanceID() != steamBreathingRange.GetInstanceID()) return;
        }

        private void OnTriggerExitImp(ImpController imp)
        {
            if (ImpsInBreathingRange.Contains(imp))
                ImpsInBreathingRange.Remove(imp);
        }
    }

}