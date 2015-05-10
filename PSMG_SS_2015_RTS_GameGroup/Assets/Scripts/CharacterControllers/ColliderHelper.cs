using UnityEngine;
using System.Collections;

public class ColliderHelper : MonoBehaviour {

    public interface ColliderHelperListener
    {
        void OnTriggerExit2D(Collider2D collider);
    }

    private ColliderHelperListener listener;

    public void RegisterListener(ColliderHelperListener listener) {
        this.listener = listener;
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        listener.OnTriggerExit2D(collider);
    }

}
