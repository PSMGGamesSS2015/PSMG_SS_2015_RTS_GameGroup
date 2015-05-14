using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The ImpManager is a subcomponent of the GameManager and manages the
/// logic behind the player-controlled imps in a level. For example,
/// it spawns imps and gets notified when an imp is selected by the player.
/// </summary>

public class ImpManager : MonoBehaviour, ImpController.ImpControllerListener {

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

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            switch (e.keyCode)
            {
                case KeyCode.Alpha1:
                    SelectProfession(ImpType.Spearman);
                    break;
                case KeyCode.Alpha2:
                    SelectProfession(ImpType.Coward);
                    break;
                case KeyCode.Alpha3:
                    SelectProfession(ImpType.Pest);
                    break;
                case KeyCode.Alpha4:
                    SelectProfession(ImpType.LadderCarrier);
                    break;
                case KeyCode.Alpha5:
                    SelectProfession(ImpType.Blaster);
                    break;
                case KeyCode.Alpha6:
                    SelectProfession(ImpType.Firebug);
                    break;
                case KeyCode.Alpha7:
                    SelectProfession(ImpType.Minnesinger);
                    break;
                case KeyCode.Alpha8:
                    SelectProfession(ImpType.Botcher);
                    break;
                case KeyCode.Alpha9:
                    SelectProfession(ImpType.Schwarzenegger);
                    break;
                case KeyCode.Alpha0:
                    SelectProfession(ImpType.Unemployed);
                    break;
            }
        }
    }

    private void SelectProfession(ImpType profession)
    {
        if (impSelected != null)
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
        currentImps++;
        imps.Add(impController);
        spawnCounter = 0f; 
    }

    void ImpController.ImpControllerListener.OnImpSelected(ImpController impController)
    {
        impSelected = impController;
    }

    void ImpController.ImpControllerListener.OnImpHurt(ImpController impController)
    {
        imps.Remove(impController);
        impController.UnregisterListener();
    }
}