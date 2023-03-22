using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cardooo.math
{
    
    public partial class Math
    {
        /// <summary>
        /// 向量三次元旋轉
        /// HINT:非原點向量請記得將結果與輸入向量原點相加
        /// </summary>
        /// <param name="originVector">單位：向量</param>
        /// <param name="rotationInDegrees">單位：角度</param>
        /// <returns>單位：向量</returns>
        public static Vector3 RotateVector(Vector3 originVector, Vector3 rotationInDegrees)
        {
            // 創建旋轉矩陣
            Matrix4x4 rotationMatrix = Matrix4x4.Rotate(Quaternion.Euler(rotationInDegrees));            
            // 將向量應用旋轉矩陣
            Vector3 rotatedVector = rotationMatrix.MultiplyVector(originVector);
            return rotatedVector;
        }

        public static Vector2 GetPointOnCircleByAngle(Vector2 center, float radius, float angle)
        {            
            float x = center.x + radius * Mathf.Cos(angle * Mathf.PI / 180);
            float y = center.y + radius * Mathf.Sin(angle * Mathf.PI / 180);
            return new Vector2(x, y);
        }

        public static Vector2 GetRandomPointOnCircle(Vector2 center, float radius)
        {
            float angle = Random.Range(0f, 360f);
            return GetPointOnCircleByAngle(center, radius, angle);
        }

        //
        //矩陣向量位移        
        //拋物線
        //重力模擬
        //
    }
}