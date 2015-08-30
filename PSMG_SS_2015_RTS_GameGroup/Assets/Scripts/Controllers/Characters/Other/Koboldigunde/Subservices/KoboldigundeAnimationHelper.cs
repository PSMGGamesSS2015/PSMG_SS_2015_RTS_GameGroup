using Assets.Scripts.AssetReferences;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Controllers.Characters.Other.Koboldigunde.Subservices
{
    public class KoboldigundeAnimationHelper : AnimationHelper
    {
        public void Start()
        {
            Play(AnimationReferences.ImpStanding);
        }
    }
}