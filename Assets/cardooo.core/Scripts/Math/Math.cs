using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cardooo.math
{
    public partial class Math
    {
        public static Vector2 GetPointOnCircleByAngle(Vector2 center, float radius, float angle)
        {            
            float x = center.x + radius * Mathf.Cos(angle * Mathf.PI / 180);
            float y = center.x + radius * Mathf.Sin(angle * Mathf.PI / 180);
            return new Vector2(x, y);
        }

        public static Vector2 GetRandomPointOnCircle(Vector2 center, float radius)
        {
            float angle = Random.Range(0f, 360f);
            return GetPointOnCircleByAngle(center, radius, angle);
        }
    }
}