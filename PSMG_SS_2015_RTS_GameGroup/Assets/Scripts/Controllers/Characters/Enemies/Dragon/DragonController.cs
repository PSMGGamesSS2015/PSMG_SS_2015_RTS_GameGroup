using Assets.Scripts.Controllers.Characters.Enemies.Dragon.Subservices;
using Assets.Scripts.Helpers;

namespace Assets.Scripts.Controllers.Characters.Enemies.Dragon
{
    public class DragonController : EnemyController {

        public void Awake()
        {
            InitServices();
        }

        private void InitServices()
        {
            gameObject.AddComponent<DragonMovementService>();
            gameObject.AddComponent<DragonSteamBreathingService>();
            gameObject.AddComponent<DragonFireBreathingService>();
            gameObject.AddComponent<AudioHelper>();
            gameObject.AddComponent<DragonCollisionService>();
        }
    }
}
