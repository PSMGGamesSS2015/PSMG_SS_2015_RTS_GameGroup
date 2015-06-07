using UnityEngine;
using System.Collections;

public class RavineController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == TagReferences.IMP)
        {
            collider.gameObject.GetComponent<ImpController>().LeaveGame();
        }
    }

}