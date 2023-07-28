using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public static class LayerManager
    {
        #region Layers
        //public static int WaterLayer = 4;
        #endregion

        #region Layer Masks

        // public static int WaterMask = 1 << WaterLayer;
        // public static int PlatformMask = DoorMask | WallMask;
        #endregion
        
        
        public static bool IsInLayerMask(int layerWantToCheck, LayerMask layerMask)
        {
            if (((1 << layerWantToCheck) & layerMask) != 0)
            {
                return true;
            }
            return false;
        }
    }

}
