using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The ImpManager is a subcomponent of the GameManager and manages the
/// logic behind the player-controlled imps in a level. For example,
/// it spawns imps and gets notified when an imp is selected by the player.
/// </summary>

public class ImpManager : MonoBehaviour, ImpController.ImpControllerListener {

    private Level lvl;

    private List<ImpController> imps;

    private float spawnCounter;
    private int currentImps;

    private ImpController impSelected;

    public GameObject impPrefab;

    private void Awake()
    {
        imps = new List<ImpController>();
    }
    
    public void SetLvl(Level lvl) {
        this.lvl = lvl;
    }

    public void SpawnImps()
    {
        if (currentImps == 0)
        {
            SpawnImp();
        }
        else if (IsMaxImpsReached() && IsSpawnTimeCooledDown())
        {
            SpawnImp();
        }
        else
        {
            spawnCounter += Time.deltaTime;
        }
    }

    private bool IsMaxImpsReached()
    {
        return currentImps < lvl.GetConfig().GetMaxImps();
    }

    private bool IsSpawnTimeCooledDown()
    {
        return spawnCounter >= lvl.GetConfig().GetSpawnInterval();
    }

    private void SpawnImp()
    {
        Vector3 spawnPosition = lvl.GetSpawnPosition();
        GameObject imp = (GameObject)Instantiate(impPrefab, spawnPosition, Quaternion.identity);
        ImpController impController = imp.GetComponent<ImpController>();
        impController.RegisterListener(this);
        currentImps++;
        spawnCounter = 0f;

    }

    void ImpController.ImpControllerListener.OnImpSelected(ImpController impController)
    {
        impSelected = impController;
        Debug.Log("An imp was selected:");
        Debug.Log(this.impSelected); 
    }
}