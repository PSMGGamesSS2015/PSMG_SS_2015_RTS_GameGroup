using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class SchwarzeneggerSpotController : MonoBehaviour
    {
        public bool IsOccupied { get; set; }

        public void Awake()
        {
            IsOccupied = false;
        }
    }
}