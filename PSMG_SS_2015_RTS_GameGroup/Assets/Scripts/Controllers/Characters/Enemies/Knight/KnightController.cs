using Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight
{
    public class KnightController : EnemyController
    {

        public void Awake()
        {
            InitServices();
        }

        private void InitServices()
        {
            gameObject.AddComponent<KnightMovementService>();
            gameObject.AddComponent<KnightAttackService>();
            gameObject.AddComponent<KnightCollisionSerivce>();
            gameObject.AddComponent<KnightEatingTartService>();
            gameObject.AddComponent<KnightAnimationHelper>();
            gameObject.AddComponent<KnightFeelsSoHotService>();
            gameObject.AddComponent<KnightAudioService>();
        }
    }
}