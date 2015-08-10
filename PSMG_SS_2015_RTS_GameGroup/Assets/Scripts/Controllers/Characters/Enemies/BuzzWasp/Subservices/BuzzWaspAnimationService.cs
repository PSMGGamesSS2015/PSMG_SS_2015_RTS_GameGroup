using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Controllers.Characters.Enemies.BuzzWasp.Subservices
{
    public class BuzzWaspAnimationService : AnimationHelper
    {
        public void Start()
        {
            Play(AnimationReferences.BuzzWaspFlyUpAndDown);
        }
    }
}