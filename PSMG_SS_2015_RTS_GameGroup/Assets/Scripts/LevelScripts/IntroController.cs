using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.LevelScripts
{
    public class IntroController : MonoBehaviour
    {
        private List<SpriteRenderer> textLines;

        public void Awake()
        {
            textLines = GetComponentsInChildren<SpriteRenderer>().Where(sr => sr.gameObject.tag == TagReferences.Paragraph).ToList();
        }

        public void FixedUpdate()
        {
            textLines.ForEach(Move);
        }

        private void Move(SpriteRenderer textLine)
        {
            var currentPositionOfText = textLine.gameObject.transform.position;

            var newPositionOfText = new Vector3(currentPositionOfText.x, currentPositionOfText.y + 0.005f, currentPositionOfText.z);

            textLine.gameObject.transform.position = newPositionOfText;

            var currentScaleOfText = textLine.gameObject.transform.localScale;

            var newScaleOfText = new Vector3(currentScaleOfText.x - 0.00045f, currentScaleOfText.y - 0.00045f, currentScaleOfText.z);

            textLine.gameObject.transform.localScale = newScaleOfText;
        }
    }
}