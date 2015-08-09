using Assets.Scripts.Controllers.Characters.Enemies.BuzzWasp.Subservices;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.BuzzWasp
{
    public class BuzzWaspController : MonoBehaviour
    {
        public void Awake()
        {
            gameObject.AddComponent<BuzzWaspMovementService>();
        }
    }
}
