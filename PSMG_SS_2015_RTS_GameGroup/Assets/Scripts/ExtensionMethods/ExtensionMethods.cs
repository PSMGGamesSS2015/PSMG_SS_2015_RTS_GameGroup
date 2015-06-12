using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void StopAllCounters(this MonoBehaviour mb)
        {
            // TODO Get component cannot be used here since GameObjects are spawned
            var counters = mb.gameObject.GetComponents<Counter>();

            for (int i = counters.Length - 1; i >= 0; i--)
            {
                counters[i].Stop();
            }
        }

        public static void Flip(this MonoBehaviour mb)
        {
            var obj = mb.gameObject;
            var newScale = obj.transform.localScale;
            newScale.x *= -1;
            obj.transform.localScale = newScale;
        }

    }
}