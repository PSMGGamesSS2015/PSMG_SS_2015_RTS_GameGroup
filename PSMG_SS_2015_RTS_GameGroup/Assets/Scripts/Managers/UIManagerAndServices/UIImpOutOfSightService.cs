using System.Collections.Generic;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIImpOutOfSightService : MonoBehaviour
    {
        public GameObject ImpOfSightIconPrefab;

        private List<ImpController> impsOutOfSight;
        private List<ImpController> impReference;

        private Dictionary<ImpController, GameObject> impOutOfSightIcons; 

        public void Awake()
        {
            impsOutOfSight = new List<ImpController>();
            impOutOfSightIcons = new Dictionary<ImpController, GameObject>();
        }

        public void Start()
        {
            impReference = ImpManager.Instance.Imps;
        }

        public void Update()
        {
            UpdateIconList();

            UpdateIconPositions();
        }

        

        private void UpdateIconList()
        {
            impReference.ForEach(CheckIfWithinCanvas);
        }

        private void CheckIfWithinCanvas(ImpController impController)
        {
            if (IsWithinCanvas(impController))
            {
                if (impOutOfSightIcons.ContainsKey(impController))
                {
                    var outOfSightIcon = impOutOfSightIcons[impController];
                    impOutOfSightIcons.Remove(impController);
                    Destroy(outOfSightIcon);
                }
            }
            else
            {
                if (!impOutOfSightIcons.ContainsKey(impController))
                {
                    var impOutOfSightIcon = Instantiate(ImpOfSightIconPrefab);
                    impOutOfSightIcons.Add(impController, impOutOfSightIcon);
                }
                
            }
        }

        private bool IsWithinCanvas(ImpController impController)
        {
            return false;
        }

        private void UpdateIconPositions()
        {
            // TODO
        }
    }
}