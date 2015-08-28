using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class CanonController : MonoBehaviour
    {
        public bool IsBeingFired { get; set; }
        private List<ParticleSystem> flare;

        public GameObject CanonBallPrefab;

        public void Awake()
        {
            IsBeingFired = false;
            flare = GetComponentsInChildren<ParticleSystem>().ToList();
        }

        public void Start()
        {
            flare.ForEach(f => f.Stop());
        }

        public void Light()
        {
            flare.ForEach(f => f.Play());
        }

        public void Fire()
        {
            flare.ForEach(f => f.Stop());

            var canonBall = Instantiate(CanonBallPrefab);
            canonBall.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 2f);
        }
    }
}