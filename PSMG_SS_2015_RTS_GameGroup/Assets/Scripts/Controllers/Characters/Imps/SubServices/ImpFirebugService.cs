using Assets.Scripts.Controllers.Objects;

namespace Assets.Scripts.Controllers.Characters.Imps.SubServices
{
    public class ImpFirebugService : ImpProfessionService
    {
        public void LightGaslight(GaslightController gaslight)
        {
            if (gaslight.IsLight) return;
            gaslight.Light();
        }
    }
}