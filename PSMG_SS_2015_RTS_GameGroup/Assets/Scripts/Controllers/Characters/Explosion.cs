using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{

    private SpriteRenderer[] components;

    private void Awake()
    {
        components = GetComponentsInChildren<SpriteRenderer>();
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