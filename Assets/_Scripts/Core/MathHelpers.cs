using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public static class MathHelpers
    {
        public static bool IsContainBound(this Bounds bound, Bounds target)
        {
            return (bound.Contains(target.min) && bound.Contains(target.max));
        }

        /// <summary>
        /// Remaps a value x in interval [A,B], to the proportional value in interval [C,D]
        /// </summary>
        /// <param name="x">The value to remap.</param>
        /// <param name="A">the minimum bound of interval [A,B] that contains the x value</param>
        /// <param name="B">the maximum bound of interval [A,B] that contains the x value</param>
        /// <param name="C">the minimum bound of target interval [C,D]</param>
        /// <param name="D">the maximum bound of target interval [C,D]</param>
        public static float Remap(float x, float A, float B, float C, float D)
        {
            var remappedValue = C + (x-A)/(B-A) * (D - C);
            return remappedValue;
        }
        
        /// <summary>
        /// Returns the angle of this vector, in radians
        /// </summary>
        /// <param name="v">The vector to get the angle of. It does not have to be normalized</param>
        /// <returns></returns>
        public static float GetAngle( this Vector2 v ) => Mathf.Atan2( v.y, v.x );
        public static float DegToRad( this float angDegrees ) => angDegrees * Mathf.Deg2Rad;
        public static float RadToDeg( this float angRadians ) => angRadians * Mathf.Rad2Deg;

        public static bool IsInclusiveRange(this float f, float min, float max)
        {
            return (min <= f) && (f <= max);
        }
        public static bool IsExclusiveRange(this float f, float min, float max)
        {
            return (min < f) && (f < max);
        }
    }

}
