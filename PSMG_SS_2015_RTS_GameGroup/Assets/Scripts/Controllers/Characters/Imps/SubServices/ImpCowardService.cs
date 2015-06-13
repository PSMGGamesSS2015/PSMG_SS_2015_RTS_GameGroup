using Assets.Scripts.Utility;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpCowardService : ImpProfessionService
    {
        
        private Counter attackCounter;
        private TriggerCollider2D attackRange;
        public ImpController CommandPartner;


        public void FormCommand(ImpController commandPartner)
        {
            CommandPartner = commandPartner;
        }

        public void DissolveCommand()
        {
            CommandPartner = null;
        }

        public bool IsInCommand()
        {
            return CommandPartner != null;
        }
    }
}