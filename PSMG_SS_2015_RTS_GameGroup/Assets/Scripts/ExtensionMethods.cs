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

}