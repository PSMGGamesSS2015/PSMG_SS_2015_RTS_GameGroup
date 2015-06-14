using Assets.Scripts.Controllers.Characters.Enemies.Knight.Subservices;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Controllers.Characters.Enemies.Knight
{
    public class Controller : EnemyController
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
            gameObject.AddComponent<KnightInteractionService>();
            gameObject.AddComponent<AnimationHelper>();
            gameObject.AddComponent<AudioHelper>();
        }
    }
}