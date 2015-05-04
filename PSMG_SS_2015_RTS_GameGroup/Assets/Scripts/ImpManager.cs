using UnityEngine;
using System.Collections;

public class ImpManager : MonoBehaviour {

    private Level lvl;

    private float spawnCounter;
    private int currentImps;

    private ImpController impSelected;

    public GameObject impPrefab;

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
        GameObject newImp = (GameObject)Instantiate(impPrefab, spawnPosition, Quaternion.identity);
        ImpController newImpController = newImp.GetComponent<ImpController>();
        newImpController.OnImpSelected += OnImpSelected;
        currentImps++;
        spawnCounter = 0f;
    }

    private void OnImpSelected(ImpController impSelected)
    {
        this.impSelected = impSelected;
        Debug.Log("An imp was selected:");
        Debug.Log(this.impSelected);
    }

}