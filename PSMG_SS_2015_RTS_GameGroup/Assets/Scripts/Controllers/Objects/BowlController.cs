using UnityEngine;

namespace Assets.Scripts.Controllers.Objects
{
    public class BowlController : MonoBehaviour
    {
        public bool HasFlourBeenAdded { get; private set; }

        public bool HasBeenBattered { get; private set; }

        public bool HasBeenHeated { get; private set; }

        public void Awake()
        {
            HasFlourBeenAdded = false;
            HasBeenBattered = false;
            HasBeenHeated = false;
        }

        public void AddFlour()
        {
            if (HasFlourBeenAdded || HasBeenBattered || HasBeenHeated) return;

            HasFlourBeenAdded = true;
            // TODO Display flour
        }

        public void BatterDough()
        {
            if (!HasFlourBeenAdded) return;
            if (HasBeenBattered || HasBeenHeated) return;

            HasBeenBattered = true;
            // TODO
        }

        public void Heat()
        {
            if (!HasFlourBeenAdded || !HasBeenBattered) return;
            if (HasBeenHeated) return;

            HasBeenHeated = true;
            // TODO 
        }
    }
}