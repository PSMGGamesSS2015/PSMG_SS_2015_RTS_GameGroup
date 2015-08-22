using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonUIService : MonoBehaviour
    {
        public void OnMouseDown()
        {
            GetComponent<DragonAudioService>().PlaySelectionSound();
        }
    }
}