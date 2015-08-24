using System.Collections;
using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices
{
    public class KnightFeelsSoHotService : MonoBehaviour
    {

        private int heatCounter;

        public void Awake()
        {
            heatCounter = 0;
        }

        public void Heat()
        {
            heatCounter++;

            StartCoroutine(HeatingRoutine());
        }

        private IEnumerator HeatingRoutine()
        {
            GetComponent<KnightAnimationHelper>().Play(AnimationReferences.KnightBeingSoHot);

            yield return new WaitForSeconds(3.8f);

            GetComponent<KnightSpriteManagerService>().ColorKnightInRed();

            yield return new WaitForSeconds(2f);

            GetComponent<KnightSpriteManagerService>().ColorKnightInDefaultColor();

            yield return new WaitForSeconds(1f);

            if (heatCounter >= 2)
            {
                GetComponent<KnightController>().Leave();
            }
            else
            {
                GetComponent<KnightAnimationHelper>().Play(AnimationReferences.KnightStanding);
            }
        }
    }
}