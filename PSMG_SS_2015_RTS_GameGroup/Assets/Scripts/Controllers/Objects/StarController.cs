using System.Collections;
using Assets.Scripts.Utility;
using UnityEngine;

// script adapted from the following source: http://answers.unity3d.com/questions/805199/how-do-i-scale-a-gameobject-over-time.html

namespace Assets.Scripts.Controllers.Objects
{
    public class StarController : MonoBehaviour
    {
        private Vector3 originalScale;
        private Vector3 destinationScale;

        public enum ScalingType
        {
            Growing,
            Shrinking
        }

        public void Awake()
        {
            originalScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            destinationScale = new Vector3(originalScale.x * 0.8f, originalScale.y * 0.8f, originalScale.z * 0.8f);
        }

        public void Start()
        {
            var rand = Random.Range(0f, 5f);
            Counter.SetCounter(gameObject, rand, ScaleOvertime, false);
        }

        private void ScaleOvertime()
        {
            StartCoroutine(ScaleOverTime(1, ScalingType.Growing));
        }

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator ScaleOverTime(float time, ScalingType scalingType)
        {
            var currentTime = 0.0f;

            do
            {
                if (scalingType == ScalingType.Growing)
                {
                    gameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime/time);
                }
                else
                {
                    gameObject.transform.localScale = Vector3.Lerp(destinationScale, originalScale, currentTime / time);
                }
                currentTime += Time.deltaTime;
                yield return null;
            } while (currentTime <= time);

            // TODO
            if (scalingType == ScalingType.Growing)
            {
                StartCoroutine(ScaleOverTime(1, ScalingType.Shrinking));
            }
            else
            {
                StartCoroutine(ScaleOverTime(1, ScalingType.Growing));
            }
        }
    }
}