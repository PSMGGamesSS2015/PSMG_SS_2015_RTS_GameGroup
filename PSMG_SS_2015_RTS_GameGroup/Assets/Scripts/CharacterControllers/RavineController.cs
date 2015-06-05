using UnityEngine;
using System.Collections;

public class RavineController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == "Imp")
        {
            Debug.Log("An Imp has fallen into a ravine");
            collider.gameObject.GetComponent<ImpController>().LeaveGame();
        }
    }

}