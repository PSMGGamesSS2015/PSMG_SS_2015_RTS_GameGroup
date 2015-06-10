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

    private SoundManager soundManager;

    private List<ImpManagerListener> listeners;

    public interface ImpManagerListener
    {
        void OnUpdateMaxProfessions(int[] professions);
    }

    public void RegisterListener(ImpManagerListener listener) 
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(ImpManagerListener listener) 
    {
        listeners.Remove(listener);
    }

    public SoundManager SoundMgr
    {
        set
        {
            soundManager = value;
        }
    }

    private void Awake()
    {
        currentImps = 0;
        imps = new List<ImpController>();
        listeners = new List<ImpManagerListener>();
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
            if (!impSelected.IsTrainable)
            {
                Debug.Log("This imp is currently not trainable");
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
    }

    public int[] getProfessions()
    {
        return professions;
    }

    public int[] getProfessionsMax()
    {
        return config.MaxProfessions;
    }

    private bool IsTrainingLimitReached(ImpType profession)
    {
        return professions[(int)profession] < config.MaxProfessions[(int)profession];
    }

    private void UpdateMaxProfessions(ImpType profession)
    {
        UpdateMaxProfessions();
        professions[(int)profession]++;
        foreach (ImpManagerListener listener in listeners)
        {
            listener.OnUpdateMaxProfessions(professions);
        }
    }

    private void UpdateMaxProfessions()
    {
        if (impSelected.Type != ImpType.Unemployed && 
            professions[(int)impSelected.Type] > 0)
        {
            professions[(int)impSelected.Type]--;
            foreach (ImpManagerListener listener in listeners)
            {
                listener.OnUpdateMaxProfessions(professions);
            }
        }
        
    }

    private void UpdateMaxProfessions(ImpController imp)
    {
        if (imp.Type != ImpType.Unemployed &&
            professions[(int)imp.Type] > 0)
        {
            professions[(int)imp.Type]--;
            foreach (ImpManagerListener listener in listeners)
            {
                listener.OnUpdateMaxProfessions(professions);
            }
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

        imps.Add(impController);
        
        spawnCounter = 0f; 
    }

    #region interface implementation
     
    void ImpController.ImpControllerListener.OnImpSelected(ImpController impController)
    {
        SelectImp(impController);
    }

    private void DisplaySelectionOfSelectedImp()
    {
        impSelected.Selection.Display();
    }

    private void HideSelectionOfAllImps()
    {
        foreach (ImpController imp in imps)
        {
            imp.Selection.Hide();
        }
    }

    private void SelectImp(ImpController imp)
    {
        impSelected = imp;
        HideSelectionOfAllImps();
        DisplaySelectionOfSelectedImp();
    }

    void ImpController.ImpControllerListener.OnImpHurt(ImpController impController)
    {
        imps.Remove(impController);
        currentImps--;
        impController.UnregisterListener(this);
    }

    void InputManager.InputManagerListener.OnDisplayImpLabels()
    {
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
        if (impSelected == null)
        {
            if (imps.Count != 0)
            {
                SelectImp(imps[(int)Random.Range(0, imps.Count - 1)]);
            }
        }
        else
        {
            int indexOfCurrentImp = imps.IndexOf(impSelected);
            int indexOfNextImp;
            if (indexOfCurrentImp >= imps.Count - 1)
            {
                indexOfNextImp = 0;
            }
            else
            {
                indexOfNextImp = indexOfCurrentImp + 1;
            }
            SelectImp(imps[indexOfNextImp]);
        }
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