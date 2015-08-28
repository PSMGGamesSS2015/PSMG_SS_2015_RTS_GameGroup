using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollUIService : MonoBehaviour
    {
        public void OnMouseDown()
        {
            GetComponent<TrollAudioService>().PlaySelectionSound();
        }
    }
}