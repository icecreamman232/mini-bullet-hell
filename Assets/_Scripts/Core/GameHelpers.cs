using System;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.Managers
{
    public static class GameHelper
    {
        #region Camera
        public static Vector2 sizeOfCamera = Vector2.zero;
        public static Vector2 GetCurrentScreenBounds()
        {
            if(sizeOfCamera == Vector2.zero)
            {
                ReCalculateSizeOfCamera();
            }
            return sizeOfCamera;
        }
        public static Vector2 HalfSizeOfCamera()
        {
            return GetCurrentScreenBounds() * 0.5f;
        }


        public static void ReCalculateSizeOfCamera()
        {
            var mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Camera Not Found!");
                return;
            }
            Vector2 A = new Vector2();
            A.y = mainCamera.orthographicSize * 2;
            A.x = (mainCamera.aspect * mainCamera.orthographicSize) * 2;
            sizeOfCamera = A;
        }

        public static bool IsInsideScreenBounds(Vector3 position)
        {
            bool result = false;
            if(position.x > -GameHelper.GetCurrentScreenBounds().x/2 && position.x < GameHelper.GetCurrentScreenBounds().x/2)
            {
                if(position.y > -GameHelper.GetCurrentScreenBounds().y/2 && position.y < GameHelper.GetCurrentScreenBounds().y/2)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        public static void SetAlpha(this Image image, float alpha)
        {
            var curColor = image.color;
            curColor.a = alpha;
            image.color = curColor;
        }

        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
