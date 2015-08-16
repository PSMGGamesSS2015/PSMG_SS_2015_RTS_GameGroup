using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Controllers.Characters.Imps;
using Assets.Scripts.Controllers.Characters.Imps.SubServices;
using UnityEngine;

namespace Assets.Scripts.Managers.UIManagerAndServices
{
    public class UIImpOutOfSightService : MonoBehaviour
    {
        public GameObject ImpOfSightIconPrefab;

        private List<ImpController> imps;
        private readonly int[] impsOutsideOfScreen = new int[4];

        private const int LeftOfCanvas = 0;
        private const int RightOfCanvas = 1;
        private const int AboveCanvas = 2;
        private const int BelowCanvas = 3;

        public Dictionary<ImpController, GameObject> ImpOutOfSightIcons;

        public void Awake()
        {
            ImpOutOfSightIcons = new Dictionary<ImpController, GameObject>();
        }

        public void Start()
        {
            imps = ImpManager.Instance.Imps;
        }

        public void Update()
        {
            UpdateIconList();
            UpdateIconPositions();
        }

        private void UpdateIconList()
        {
            for (var i = 0; i < impsOutsideOfScreen.Length; i++)
            {
                impsOutsideOfScreen[i] = 0;
            }
            imps.ForEach(CheckIfWithinCanvas);
        }

        private void CheckIfWithinCanvas(ImpController impController)
        {
            if (IsVisibleOnScreen(impController))
            {
                CheckIfOutOfSightIconHasToBeRemoved(impController);
            }
            else
            {
                CheckIfOutOfSightIconHasToBeAdded(impController);
            }
        }

        private void CheckIfOutOfSightIconHasToBeAdded(ImpController impController)
        {
            if (ImpOutOfSightIcons.ContainsKey(impController))
            {
                DisplayCorrectProfessionIcon(impController);
            }
            else
            {
                var impOutOfSightIcon = Instantiate(ImpOfSightIconPrefab); // creating new icon
                DisplayCorrectProfessionIcon(impController, impOutOfSightIcon);
                ImpOutOfSightIcons.Add(impController, impOutOfSightIcon);
            }
        }

        private void DisplayCorrectProfessionIcon(ImpController impController)
        {
            var impOutOfSightIcon = ImpOutOfSightIcons[impController].GetComponent<ImpOutOfSightIconController>();
            impOutOfSightIcon.ProfessionIcons.ForEach(pi => pi.GetComponent<SpriteRenderer>().enabled = false);
            var impType = impController.GetComponent<ImpTrainingService>().Type;
            var icon = impOutOfSightIcon.ProfessionIcons.First(pi => pi.ImpType == impType);
            icon.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void DisplayCorrectProfessionIcon(ImpController impController, GameObject impOutOfSightIcon)
        {
            var impOutOfSightIconController = impOutOfSightIcon.GetComponent<ImpOutOfSightIconController>();
            var impType = impController.GetComponent<ImpTrainingService>().Type;
            var icon = impOutOfSightIconController.ProfessionIcons.First(pi => pi.ImpType == impType);
            icon.GetComponent<SpriteRenderer>().enabled = true;
        }

        private void CheckIfOutOfSightIconHasToBeRemoved(ImpController impController)
        {
            if (!ImpOutOfSightIcons.ContainsKey(impController)) return;

            var outOfSightIcon = ImpOutOfSightIcons[impController];
            ImpOutOfSightIcons.Remove(impController);
            Destroy(outOfSightIcon);
        }

        private bool IsVisibleOnScreen(ImpController impController)
        {
            return impController.GetComponent<ImpSpriteManagerService>().Sprites[0].isVisible;
        }

        private void UpdateIconPositions()
        {
            ImpOutOfSightIcons.Keys.ToList().ForEach(UpdatePosition);
        }

        private void UpdatePosition(ImpController imp)
        {
            var screenPositionOfImp = Camera.main.WorldToScreenPoint(imp.gameObject.transform.position);
            var centerOfScreen = Camera.main.WorldToScreenPoint(Camera.main.transform.position);
            var leftMarginOfScreen = new Vector3(centerOfScreen.x - Screen.width/2.0f, centerOfScreen.y,
                centerOfScreen.z);
            var rightMarginOfScreen = new Vector3(centerOfScreen.x + Screen.width/2.0f, centerOfScreen.y,
                centerOfScreen.z);
            var topMarginOfScreen = new Vector3(centerOfScreen.x, centerOfScreen.y + Screen.height/2.0f,
                centerOfScreen.z);
            var bottomMarginOfScreen = new Vector3(centerOfScreen.x, centerOfScreen.y - Screen.height/2.0f,
                centerOfScreen.z);

            if (CheckIfImpIsLeftOfScreen(imp, screenPositionOfImp, leftMarginOfScreen)) return;

            if (CheckIfImpIsRightOfScreen(imp, screenPositionOfImp, rightMarginOfScreen)) return;

            if (CheckIfImpIsAboveScreen(imp, screenPositionOfImp, topMarginOfScreen)) return;

            CheckIfImpIsBelowScreen(imp, screenPositionOfImp, bottomMarginOfScreen);
        }

        private bool CheckIfImpIsLeftOfScreen(ImpController imp, Vector3 screenPositionOfImp, Vector3 leftMarginOfScreen)
        {
            if (!(screenPositionOfImp.x < leftMarginOfScreen.x)) return false;

            impsOutsideOfScreen[LeftOfCanvas]++;

            ImpOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().PointerLeft.GetComponent<SpriteRenderer>().enabled = true;

            var pos =
                Camera.main.ScreenToWorldPoint(new Vector3(leftMarginOfScreen.x + 45,
                    100 + 100*impsOutsideOfScreen[LeftOfCanvas], leftMarginOfScreen.z));

            ImpOutOfSightIcons[imp].transform.position = new Vector3(pos.x, pos.y, 0f);
            return true;
        }

        private bool CheckIfImpIsRightOfScreen(ImpController imp, Vector3 screenPositionOfImp,
            Vector3 rightMarginOfScreen)
        {
            if (!(screenPositionOfImp.x > rightMarginOfScreen.x)) return false;

            impsOutsideOfScreen[RightOfCanvas]++;

            ImpOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().PointerRight.GetComponent<SpriteRenderer>().enabled = true;

            var pos =
                Camera.main.ScreenToWorldPoint(new Vector3(rightMarginOfScreen.x - 45,
                    100 + 100*impsOutsideOfScreen[RightOfCanvas], rightMarginOfScreen.z));

            ImpOutOfSightIcons[imp].transform.position = new Vector3(pos.x, pos.y, 0f);
            return true;
        }

        private bool CheckIfImpIsAboveScreen(ImpController imp, Vector3 screenPositionOfImp, Vector3 topMarginOfScreen)
        {
            if (!(screenPositionOfImp.y > topMarginOfScreen.y)) return false;

            impsOutsideOfScreen[AboveCanvas]++;

            ImpOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().PointerUp.GetComponent<SpriteRenderer>().enabled = true;

            var pos =
                Camera.main.ScreenToWorldPoint(new Vector3(100 + 100*impsOutsideOfScreen[AboveCanvas],
                    topMarginOfScreen.y - 60, topMarginOfScreen.z));

            ImpOutOfSightIcons[imp].transform.position = new Vector3(pos.x, pos.y, 0f);
            return true;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private bool CheckIfImpIsBelowScreen(ImpController imp, Vector3 screenPositionOfImp,
            Vector3 bottomMarginOfScreen)
        {
            if (!(screenPositionOfImp.y < bottomMarginOfScreen.y)) return false;

            impsOutsideOfScreen[BelowCanvas]++;

            ImpOutOfSightIcons[imp].GetComponent<ImpOutOfSightIconController>().PointerDown.GetComponent<SpriteRenderer>().enabled = true;

            var pos =
                Camera.main.ScreenToWorldPoint(new Vector3(0 + 60*impsOutsideOfScreen[BelowCanvas],
                    bottomMarginOfScreen.y + 100, bottomMarginOfScreen.z));

            ImpOutOfSightIcons[imp].transform.position = new Vector3(pos.x, pos.y, 0f);
            return true;
        }

        public void OnImpHurt(ImpController impController)
        {
            GameObject value;

            var isGettingValue = ImpOutOfSightIcons.TryGetValue(impController, out value);

            if (isGettingValue)
            {
                var impOutOfSightIcon = ImpOutOfSightIcons[impController];
                ImpOutOfSightIcons.Remove(impController);
                if (impOutOfSightIcon != null) Destroy(impOutOfSightIcon);
            }
            else
            {
                ImpOutOfSightIcons.Values.ToList().ForEach(v => Destroy(v.gameObject));
                ImpOutOfSightIcons.Clear();
            }
        }
    }
}