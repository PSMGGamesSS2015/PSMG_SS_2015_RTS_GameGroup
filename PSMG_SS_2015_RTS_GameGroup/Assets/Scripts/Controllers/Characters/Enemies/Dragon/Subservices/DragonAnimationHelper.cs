using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices
{
    public class DragonAnimationHelper : AnimationHelper
    {
        public void Start()
        {
            Play(AnimationReferences.DragonFlying);
        }

        public void PlayFlyingAnimation()
        {
            Play(AnimationReferences.DragonFlying);
        }

        public void PlayBreathingAnimation()
        {
            Play(AnimationReferences.DragonBreathing);
        }

        public void PlayDefeatedAnimation()
        {
            Play(AnimationReferences.DragonDead);
        }
    }
}