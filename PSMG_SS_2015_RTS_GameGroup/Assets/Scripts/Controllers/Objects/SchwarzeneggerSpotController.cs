using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class SchwarzeneggerSpotController : MonoBehaviour
    {
        public bool IsOccupied { get; set; }

        public MovingObject.Direction Direction;

        public void Awake()
        {
            IsOccupied = false;
        }
    }
}