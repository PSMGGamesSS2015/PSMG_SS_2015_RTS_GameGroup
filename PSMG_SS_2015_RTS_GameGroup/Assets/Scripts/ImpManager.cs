using UnityEngine;
using System.Collections;

public class ImpManager : MonoBehaviour, ImpController.ImpControllerListener {

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