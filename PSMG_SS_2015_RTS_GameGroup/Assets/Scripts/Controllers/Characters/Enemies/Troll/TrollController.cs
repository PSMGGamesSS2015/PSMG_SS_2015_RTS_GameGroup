using Assets.Scripts.Types;

namespace Assets.Scripts.Controllers.Characters.Enemies.Troll
{
    public class TrollController : EnemyController
    {
        public EnemyType Type;

        public ITrollControllerListener Listener { get; private set; }

        public interface ITrollControllerListener
        {
            void OnEnemyHurt(TrollController trollController);
        }

        public void RegisterListener(ITrollControllerListener listener)
        {
            Listener = listener;
        }

        public void UnregisterListener()
        {
            Listener = null;
        }

        public void Awake()
        {
            InitServices();
        }

        private void InitServices()
        {
            gameObject.AddComponent<TrollAttackService>();
            gameObject.AddComponent<TrollMoodService>();
        }

    }
}