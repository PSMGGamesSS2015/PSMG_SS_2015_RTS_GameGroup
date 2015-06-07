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
            if (renderers[i].gameObject.tag == TagReferences.IMP_INVENTORY_SPEAR)
            {
                spear = renderers[i];
            }
            if (renderers[i].gameObject.tag == TagReferences.IMP_INVENTORY_SHIELD)
            {
                shield = renderers[i];
            }
            if (renderers[i].gameObject.tag == TagReferences.IMP_INVENTORY_BOMB)
            {
                bomb = renderers[i];
            }
            if (renderers[i].gameObject.tag == TagReferences.IMP_INVENTORY_LADDER)
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

    public Explosion Explo
    {
        get
        {
            return explosion;
        }
    }

    public void Display(string item)
    {
        HideAllTools();
        switch (item)
        {
            case "Spear":
                DisplaySpear();
                break;
            case "Shield":
                DisplayShield();
                break;
            case "Ladder":
                DisplayLadder();
                break;
            case "Bomb":
                DisplayBomb();
                break;
            case "Explosion":
                explosion.Display();
                break;
            default:
                break;
        }
        
    }

    public void DisplaySpear()
    {
        spear.enabled = true;
    }


    public void DisplayLadder()
    {
        ladder.enabled = true;
    }

    public void DisplayBomb()
    {
        bomb.enabled = true;
    }

    public void DisplayShield()
    {
        shield.enabled = true;
    }

    public void DisplayExplosion()
    {
        explosion.Display();
    }
}