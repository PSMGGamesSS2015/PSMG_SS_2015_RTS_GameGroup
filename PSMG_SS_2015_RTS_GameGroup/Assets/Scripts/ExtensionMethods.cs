using UnityEngine;
using System.Collections;

public static class ExtensionMethods
{
    public static void StopAllCounters(this MonoBehaviour mb)
    {
        Counter[] counters = mb.gameObject.GetComponents<Counter>();

        foreach (Counter counter in counters)
        {
            counter.Stop();
        }
    }

    public static void Flip(this MonoBehaviour mb)
    {
        GameObject obj = mb.gameObject;
        Vector3 newScale = obj.transform.localScale;
        newScale.x *= -1;
        obj.transform.localScale = newScale;
    }

}