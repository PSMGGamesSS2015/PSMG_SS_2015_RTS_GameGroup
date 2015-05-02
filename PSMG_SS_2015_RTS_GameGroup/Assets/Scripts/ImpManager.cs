using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImpManager : MonoBehavior
{
    // TODO add controls
    
    List<ImpController> impControllers;

    public GameObject impPrefab;
    public const string[] PROFESSIONS = {
                                            "Spearman",
                                            "Guardian",
                                            "LadderCarrier",
                                            "Blaster"
                                   };

    int[] professionals;

    void Awake()
    {
        imps = new List<GameObject>();
        impControllers = new List<ImpController>();
        professionals = new int[PROFESSIONS.Length];
    }

    public void SpawnImp()
    {
        GameObject imp = Instantiate(impPrefab); // TODO indicate position
        ImpController impController = imp.GetComponent<ImpController>();
        impControllers.Add(impController);
    }

    public void TrainImp(int indexOfImp, int professionCode)
    {
        // test necessary
        
        impControllers[indexOfImp].Train(professionCode);
        professionals[professionCode]++; // TODO test necessary
    }
}