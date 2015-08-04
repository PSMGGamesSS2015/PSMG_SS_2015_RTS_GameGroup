using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using Assets.Scripts.Types;
using UnityEngine;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIImpOutOfSightService : MonoBehaviour
    {
        public GameObject ImpOfSightIconPrefab;
        
        private List<ImpController> impReference;
        private readonly int[] positionsRelativeToCanvas = new int[4];

        private const int LeftOfCanvas = 0;
        private const int RightOfCanvas = 1;
        private const int AboveCanvas = 2;
        private const int BelowCanvas = 3;

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
            for (var i = 0; i < positionsRelativeToCanvas.Length; i++)
            {
                positionsRelativeToCanvas[i] = 0;
            }
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

                    switch (impController.GetComponent<ImpTrainingService>().Type)
                    {
                        case ImpType.Unemployed:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_Default.enabled = true;
                            break;
                        case ImpType.Spearman:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_Spearman.enabled = true;
                            break;
                        case ImpType.Coward:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_Coward.enabled = true;
                            break;
                        case ImpType.LadderCarrier:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_LadderCarrier.enabled = true;
                            break;
                        case ImpType.Blaster:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_Blaster.enabled = true;
                            break;
                        case ImpType.Firebug:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_Firebug.enabled = true;
                            break;
                        case ImpType.Schwarzenegger:
                            impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>().Icon_Schwarzenegger.enabled = true;
                            break;
                    }
                    

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
                positionsRelativeToCanvas[LeftOfCanvas]++;
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerLeft.enabled = true;

                impOutOfSightIcons[imp].transform.localPosition = new Vector3( // position message within canvas
                pos.x - width / 1.5f,
                pos.y - height / 2f + 80f * positionsRelativeToCanvas[LeftOfCanvas],
                pos.z
                );
            }

            if (screenPos.x > pos.x + width / 2)
            {
                positionsRelativeToCanvas[RightOfCanvas]++;
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerRight.enabled = true;

                impOutOfSightIcons[imp].transform.localPosition = new Vector3( // position message within canvas
                pos.x + width / 1.5f,
                pos.y - height / 2f + 80f * positionsRelativeToCanvas[RightOfCanvas],
                pos.z
                );

            }

            if (screenPos.y > pos.y + height/2)
            {
                positionsRelativeToCanvas[AboveCanvas]++;
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerDown.enabled = true;
                // TODO
            }

            if (screenPos.y < pos.y - height / 2)
            {
                positionsRelativeToCanvas[BelowCanvas]++;
                impOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().pointerUp.enabled = true;
                // TODO
            }

            // TODO Display right item
        }
    }
}