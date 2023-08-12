using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public static class LayerManager
    {
        #region Layers
        public static int PlayerLayer = 6;
        public static int EnemyProjectileLayer = 7;
        public static int EnemyLayer = 8;
        public static int PlayerProjectileLayer = 9;
        public static int ItemLayer = 10;
        #endregion

        #region Layer Masks

        public static int EnemyMask = 1 << EnemyLayer;
        public static int EnemyProjectileMask = 1 << EnemyProjectileLayer;
        public static int PlayerMask = 1 << PlayerLayer;
        public static int PlayerProjectileMask = 1 << PlayerProjectileLayer;
        public static int ItemMask = 1 << ItemLayer;
        //public static int PlayerMask = DoorMask | WallMask;
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
