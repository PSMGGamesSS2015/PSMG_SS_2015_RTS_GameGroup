using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class LadderSpotController : MonoBehaviour {
    
        private bool isLadderPlaced;

        public void Awake()
        {
            isLadderPlaced = false;
        }

        public bool IsLadderPlaced
        {
            get
            {
                return isLadderPlaced;
            }
        }

        public void PlaceLadder() {
            isLadderPlaced = true;
        }

    }
}
