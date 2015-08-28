using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightUIService : MonoBehaviour
    {
        public void OnMouseDown()
        {
            GetComponent<KnightAudioService>().PlaySelectionSound();
        }
    }
}