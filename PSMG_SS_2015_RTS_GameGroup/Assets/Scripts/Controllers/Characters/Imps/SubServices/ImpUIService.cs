using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpUIService : MonoBehaviour
    {
        public ImpSelection Selection { get; private set; }

        public void Awake()
        {
            Selection = GetComponentInChildren<ImpSelection>();
        }

        public void OnMouseDown()
        {
            var impController = GetComponent<ImpController>();
            impController.Listeners.ForEach(l => l.OnImpSelected(impController));
        }
    }
}