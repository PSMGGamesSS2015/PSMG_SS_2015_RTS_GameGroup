﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The imp inventory contains the sprite renderers of the tools
/// that an imp uses. 
/// </summary>

public class ImpInventory : MonoBehaviour
{
    private SpriteRenderer spear;
    private SpriteRenderer shield;
    private SpriteRenderer bomb;
    private SpriteRenderer ladder;

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
            if (renderers[i].gameObject.tag == "Spear") spear = renderers[i];
            if (renderers[i].gameObject.tag == "Shield") shield = renderers[i];
            if (renderers[i].gameObject.tag == "Bomb") bomb = renderers[i];
            if (renderers[i].gameObject.tag == "Ladder") ladder = renderers[i];
            tools.Add(renderers[i]);
        }
    }
    
    private void HideAllTools()
    {
        foreach (SpriteRenderer renderer in tools)
        {
            renderer.enabled = false;
        }
    }

    #endregion

    // TODO provisional
    public void DisplaySpear()
    {
        spear.enabled = true;
    }

}