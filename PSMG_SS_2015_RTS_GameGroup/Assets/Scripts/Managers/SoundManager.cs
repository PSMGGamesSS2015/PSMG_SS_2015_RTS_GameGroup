using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour, ImpController.ImpControllerListener
{

    void ImpController.ImpControllerListener.OnImpSelected(ImpController impController)
    {
        // TODO
        Debug.Log("Play sound for OnImpSelected");
    }

    void ImpController.ImpControllerListener.OnImpHurt(ImpController impController)
    {
        // TODO
        Debug.Log("Play sound for OnImpHurt");
    }

    void ImpController.ImpControllerListener.OnUntrain(ImpController impController)
    {
        // TODO
        Debug.Log("Play sound for OnUntrain");
    }
}