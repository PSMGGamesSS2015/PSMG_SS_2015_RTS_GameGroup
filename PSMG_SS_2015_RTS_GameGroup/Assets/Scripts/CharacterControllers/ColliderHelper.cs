using UnityEngine;
using System.Collections;

public class ColliderHelper : MonoBehaviour {

    // TODO Give this object's collider the same size as the parent's collider

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
