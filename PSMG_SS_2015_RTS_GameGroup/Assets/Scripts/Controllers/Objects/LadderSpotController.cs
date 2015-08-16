using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class LadderSpotController : MonoBehaviour {
        
        public void Awake()
        {
            IsLadderPlaced = false;
        }

        public bool IsLadderPlaced { get; set; }

    }
}
