using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers.Characters.Imps;
using UnityEngine;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIImpOutOfSightService : MonoBehaviour
    {
        public GameObject ImpOfSightIconPrefab;
        
        private List<ImpController> impReference;

        public Dictionary<ImpController, GameObject> impOutOfSightIcons;

        public void Awake()
        {
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

                    var canvas = GetComponent<UIManager>().CurrentUserInterface.UICanvas;
                    impOutOfSightIcon.transform.SetParent(canvas.transform, false); // set canvas as parent element

                    impOutOfSightIcons.Add(impController, impOutOfSightIcon);
                }
                
            }
        }

        private bool IsWithinCanvas(ImpController impController)
        {
            return impController.GetComponent<ImpAnimationHelper>().Sprites[0].isVisible;
        }

        private void UpdateIconPositions()
        {
            impOutOfSightIcons.Keys.ToList().ForEach(UpdatePosition);
        }

        private void UpdatePosition(ImpController imp)
        {
            
            var canvas = GetComponent<UIManager>().CurrentUserInterface.UICanvas;
            var pos = canvas.transform.position;
            var width = canvas.GetComponent<RectTransform>().rect.width;
            var height = canvas.GetComponent<RectTransform>().rect.height;

            var screenPos = Camera.main.WorldToScreenPoint(imp.gameObject.transform.position);

            if (screenPos.x < pos.x - width/2)
            {
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerLeft.enabled = true;
            }

            if (screenPos.x > pos.x + width / 2)
            {
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerRight.enabled = true;
            }

            if (screenPos.y > pos.y + height/2)
            {
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerDown.enabled = true;
            }

            if (screenPos.y < pos.y - height / 2)
            {
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerUp.enabled = true;
            }

            // TODO Display right item
        }
    }
}