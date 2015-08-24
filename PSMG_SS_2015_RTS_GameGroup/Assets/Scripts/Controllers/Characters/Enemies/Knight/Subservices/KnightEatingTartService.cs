using Assets.Scripts.Controllers.Objects;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightEatingTartService : MonoBehaviour
    {
        public bool IsEatingTart { get; private set; }

        public void Awake()
        {
            IsEatingTart = false;
        }

        public void EatTart(TastyTartController tart)
        {
            if (IsEatingTart) return;

            IsEatingTart = true;

            // TODO play eating tart animation

            // TODO let the imps pass
        }

        
    }
}