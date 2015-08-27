using Assets.Scripts.AssetReferences;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class Physics2DManager : MonoBehaviour
    {
        public void SetupCollisionManagement()
        {
            Physics2D.IgnoreLayerCollision(LayerReferences.IgnoreRaycastLayer, LayerReferences.IgnoreRaycastLayer, true);
            Physics2D.IgnoreLayerCollision(LayerReferences.IgnoreRaycastLayer, LayerReferences.ObjectOfInterestLayer, true);
            Physics2D.IgnoreLayerCollision(LayerReferences.ImpLayer, LayerReferences.DecorationLayerBackground, true);
            Physics2D.IgnoreLayerCollision(LayerReferences.ImpLayer, LayerReferences.DecorationLayerForeground, true);
            Physics2D.IgnoreLayerCollision(LayerReferences.DefaultLayer, LayerReferences.DecorationLayerForeground, true);
            Physics2D.IgnoreLayerCollision(LayerReferences.KnightLayer, LayerReferences.DecorationLayerBackground, true);
            Physics2D.IgnoreLayerCollision(LayerReferences.KnightLayer, LayerReferences.DecorationLayerForeground, true);
        }
    }
}