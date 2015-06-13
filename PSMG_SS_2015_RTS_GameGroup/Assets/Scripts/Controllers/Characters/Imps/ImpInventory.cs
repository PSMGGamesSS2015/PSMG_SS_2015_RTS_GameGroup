using Assets.Scripts.AssetReferences;

namespace Assets.Scripts.Controllers.Characters.Imps
{
    /// <summary>
    /// The imp inventory contains the sprite renderers of the tools
    /// used by an imp. 
    /// </summary>

    public class ImpInventory : Inventory
    {
        protected override void InitTagNames()
        {
            TagNames = new []{
                TagReferences.ImpInventorySpear, 
                TagReferences.ImpInventoryShield, 
                TagReferences.ImpInventoryBomb, 
                TagReferences.ImpInventoryLadder
            };
        }

        public override void HideItems()
        {
            base.HideItems();

            Explosion.Hide();
        }

        protected override void RetrieveItems()
        {
            base.RetrieveItems();

            Explosion = GetComponentInChildren<Explosion>();
        }

        public Explosion Explosion { get; private set; }

        public void DisplayExplosion()
        {
            Explosion.Display();
        }
    }
}