using UnityEngine;
using System.Collections;

public class LadderSpotController : MonoBehaviour {
    
    private bool isLadderPlaced;

    private void Awake()
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
