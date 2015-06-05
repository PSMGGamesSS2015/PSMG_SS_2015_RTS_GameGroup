using UnityEngine;
using System.Collections;

public class ImpSelection : MonoBehaviour {

    private SpriteRenderer[] components;

    private void Awake()
    {
        components = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Hide();
    }

    public void Display()
    {
        foreach (SpriteRenderer r in components)
        {
            r.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (SpriteRenderer r in components)
        {
            r.enabled = false;
        }
    }

}
