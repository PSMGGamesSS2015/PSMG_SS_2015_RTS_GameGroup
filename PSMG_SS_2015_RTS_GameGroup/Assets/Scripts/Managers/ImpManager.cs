using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The ImpManager is a subcomponent of the GameManager and manages the
/// logic behind the player-controlled imps in a level. For example,
/// it spawns imps and gets notified when an imp is selected by the player.
/// </summary>

public class ImpManager : MonoBehaviour, ImpController.ImpControllerListener, LevelManager.LevelManagerListener, InputManager.InputManagerListener {

    private LevelConfig config;
    private GameObject start;

    private List<ImpController> imps;
   
    private float spawnCounter;
    private int currentImps;
    private int[] professions;

    private ImpController impSelected;

    public GameObject impPrefab;

    private void Awake()
    {
        currentImps = 0;
        imps = new List<ImpController>();
    }

    public void SetLevelConfig(LevelConfig config, GameObject start)
    {
        currentImps = 0;
        this.config = config;
        this.start = start;
        professions = new int[9];
    }

    private void SelectProfession(ImpType profession)
    {
        if (impSelected == null)
        {
            Debug.Log("No imp selected");
        }
        else
        {
            if (impSelected.Type == profession)
            {
                Debug.Log("The selected imp already has that profession.");
            }
            else
            {
                    if (profession != ImpType.Unemployed)
                    {
                        if (IsTrainingLimitReached(profession))
                        {
                            UpdateMaxProfessions(profession);
                            impSelected.Train(profession);
                        }
                        else
                        {
                            Debug.Log("You cannot train anymore imps of that profession.");
                        }
                    }
                    else
                    {
                        UpdateMaxProfessions();
                        impSelected.Train(profession);
                    }
                
            }
        }
    }

    private bool IsTrainingLimitReached(ImpType profession)
    {
        return professions[(int)profession] < config.MaxProfessions[(int)profession];
    }

    private void UpdateMaxProfessions(ImpType profession)
    {
        UpdateMaxProfessions();
        professions[(int)profession]++;
    }

    private void UpdateMaxProfessions()
    {
        if (impSelected.Type != ImpType.Unemployed && 
            professions[(int)impSelected.Type] > 0)
        {
            professions[(int)impSelected.Type]--;
        }
    }

    private void UpdateMaxProfessions(ImpController imp)
    {
        if (imp.Type != ImpType.Unemployed &&
            professions[(int)imp.Type] > 0)
        {
            professions[(int)imp.Type]--;
        }
        imp.Train(ImpType.Unemployed);
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
        return currentImps < config.MaxImps;
    }

    private bool IsSpawnTimeCooledDown()
    {
        return spawnCounter >= config.SpawnInterval;
    }

    private void SpawnImp()
    {
        Vector3 spawnPosition = start.transform.position;
        GameObject imp = (GameObject)Instantiate(impPrefab, spawnPosition, Quaternion.identity);
        ImpController impController = imp.GetComponent<ImpController>();
        impController.RegisterListener(this);
        impController.MoveToSortingLayerPosition(currentImps);
        currentImps++;
        // TODO Add sorting layer stuff
        imps.Add(impController);
        
        
        spawnCounter = 0f; 
    }

    #region interface implementation
     
    void ImpController.ImpControllerListener.OnImpSelected(ImpController impController)
    {
        impSelected = impController;
    }

    void ImpController.ImpControllerListener.OnImpHurt(ImpController impController)
    {
        imps.Remove(impController);
        impController.UnregisterListener();
    }

    void InputManager.InputManagerListener.OnDisplayImpLabels()
    {
        Debug.Log("DisplayImpLabels");
        foreach (ImpController imp in imps)
        {
            imp.DisplayLabel();
        }
    }

    void InputManager.InputManagerListener.OnProfessionSelected(ImpType profession)
    {
        SelectProfession(profession);
    }

    void InputManager.InputManagerListener.OnSelectNextImp()
    {
        throw new System.NotImplementedException();
    }

    void LevelManager.LevelManagerListener.OnLevelStarted(LevelConfig config, GameObject start)
    {
        SetLevelConfig(config, start);
    }

    #endregion 

    public void OnUntrain(ImpController impController)
    {
        UpdateMaxProfessions(impController);
    }

    void InputManager.InputManagerListener.OnDismissImpLabels()
    {
        foreach (ImpController imp in imps)
        {
            imp.DismissLabel();
        }
    }
}