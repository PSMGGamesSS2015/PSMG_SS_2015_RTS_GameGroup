using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class FragileWallController : MonoBehaviour
    {
        private List<Rigidbody2D> stones; 

        public void Awake()
        {
            stones = GetComponentsInChildren<Rigidbody2D>().ToList();
        }

        public void Detonate()
        {
            // TODO make rigidbodies non-kinematic

            // TODO apply force
        }
    }
}