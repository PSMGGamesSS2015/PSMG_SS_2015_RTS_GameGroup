using UnityEngine;

#pragma warning disable 108,114

// Adapted from the following source: http://answers.unity3d.com/questions/34739/how-to-make-a-light-flicker.html

namespace Assets.Scripts
{
    public class Ambiance : MonoBehaviour
    {
        private Light light;

        private readonly float[] smoothing = new float[40];

        private float mulitplicationFactor;

        public void Awake()
        {
            light = GetComponent<Light>();
            mulitplicationFactor = light.intensity;
        }

        public void Start()
        {
            for (var i = 0; i < smoothing.Length; i++)
            {
                smoothing[i] = .0f;
            }
        }

        public void Update()
        {
            var sum = .0f;

            // Shift values in the table so that the new one is at the end and the older one is deleted.
            for (var i = 1; i < smoothing.Length; i++)
            {
                smoothing[i - 1] = smoothing[i];
                sum += smoothing[i - 1];
            }

            // Add the new value at the end of the array.
            smoothing[smoothing.Length - 1] = Random.value;
            sum += smoothing[smoothing.Length - 1];

            // Compute the average of the array and assign it to the light intensity.
            light.intensity = sum*mulitplicationFactor/smoothing.Length;
        }
    }
}