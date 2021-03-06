﻿using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonCollisionService : MonoBehaviour
    {
        public void Awake()
        {
            SetupCollisionManagement();
        }

        private void SetupCollisionManagement()
        {
            Physics2D.IgnoreLayerCollision(LayerReferences.DragonLayer, LayerReferences.ImpLayer, true);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == TagReferences.Imp) return;

            if (HasReachedTop())
            {
                if (GetComponent<DragonController>().IsWounded)
                {
                    if (GetComponent<DragonFireBreathingService>().BreathingCounter >= 3 || GetComponent<DragonFireBreathingService>().IsFirstBreath)
                    {
                        GetComponent<DragonFireBreathingService>().BreathFire();
                    }
                    else
                    {
                        GetComponent<DragonFireBreathingService>().BreathingCounter++;
                        GetComponent<DragonMovementService>().Flydown();
                    }
                }
                else
                {
                    GetComponent<DragonMovementService>().Flydown();
                }
                
            }
            else
            {
                GetComponent<DragonSteamBreathingService>().BreathSteam();
            }
        }

        private bool HasReachedTop()
        {
            return GetComponent<DragonMovementService>().CurrentDirection == MovingObject.Direction.Upwards;
        }
    }
}