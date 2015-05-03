using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImpManager : MonoBehavior
{
    // TODO add controls
    
    List<ImpController> impControllers;
    ImpController impSelected;

    public GameObject impPrefab;
    public const string[] PROFESSIONS = {
                                            "Spearman",
                                            "Guardian",
                                            "LadderCarrier",
                                            "Blaster"
                                   };

    ImpController.JOB[] professionals;

    void Awake()
    {
        imps = new List<GameObject>();
        impControllers = new List<ImpController>();
        professionals = new ImpController.JOB[PROFESSIONS.Length];
    }

    public void SpawnImp()
    {
        GameObject imp = Instantiate(impPrefab); // TODO indicate position
        ImpController impController = imp.GetComponent<ImpController>();
        RegisterEvents(impController);
        impControllers.Add(impController);
    }

    private void RegisterEvents(ImpController impController)
    {
        impController.OnSelect += SelectImp;
        // TODO unregister event at good position
    }

    void SelectImp(ImpController impController) {
        impSelected = impController;
    }

    public void TrainImp(int indexOfImp, ImpController.JOB job)
    {
        impControllers[indexOfImp].Train(job);
        professionals[(int) job]++;
    }
}