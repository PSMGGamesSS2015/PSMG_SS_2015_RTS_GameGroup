using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The imp inventory contains the sprite renderers of the tools
/// used by an imp. 
/// </summary>

public class ImpInventory : MonoBehaviour
{
    private SpriteRenderer spear;
    private SpriteRenderer shield;
    private SpriteRenderer bomb;
    private SpriteRenderer ladder;
    private Explosion explosion;

    private List<SpriteRenderer> tools;

    #region initialization

    private void Awake()
    {
        tools = new List<SpriteRenderer>();
    }

    private void Start()
    {
        RetrieveTools();
        HideAllTools();
    }

    private void RetrieveTools()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].gameObject.tag == "Spear")
            {
                spear = renderers[i];
            }
            if (renderers[i].gameObject.tag == "Shield")
            {
                shield = renderers[i];
            }
            if (renderers[i].gameObject.tag == "Bomb")
            {
                bomb = renderers[i];
            }
            if (renderers[i].gameObject.tag == "Ladder")
            {
                ladder = renderers[i];
            }
            tools.Add(renderers[i]);
        }

        explosion = GetComponentInChildren<Explosion>();
    }
    
    public void HideAllTools()
    {
        foreach (SpriteRenderer renderer in tools)
        {
            renderer.enabled = false;
        }
    }

    #endregion

    public void DisplaySpear()
    {
        HideAllTools();
        spear.enabled = true;
    }


    public void DisplayLadder()
    {
        HideAllTools();
        ladder.enabled = true;
    }

    public void DisplayBomb()
    {
        HideAllTools();
        bomb.enabled = true;
    }

    public void DisplayShield()
    {
        HideAllTools();
        shield.enabled = true;
    }

    public void DisplayExplosion()
    {
        HideAllTools();
        explosion.Display();
    }
}