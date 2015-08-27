using Assets.Scripts.Controllers.Characters.Other.Koboldigunde.Subservices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Other.Koboldigunde
{
    public class KoboldigundeController : MonoBehaviour
    {
        public void Awake()
        {
            gameObject.AddComponent<KoboldigundeAudioService>();
            gameObject.AddComponent<KoboldigundeMovementService>();
            gameObject.AddComponent<KoboldigundeAnimationHelper>();
            gameObject.AddComponent<KoboldigundeCollisionService>();
        }
    }
}