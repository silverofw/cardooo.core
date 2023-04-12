using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardooo.math;

public class MatrixTest : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;


    public Vector3 rotVector;

    [ContextMenu("DotTest")]
    void dotTest()
    {
        Debug.Log("Dot = " + Vector3.Dot(startPos, endPos));

        var dot = startPos.x * endPos.x + startPos.y * endPos.y + startPos.z * endPos.z;
        Debug.Log("Dot = " + dot);
    }

    [ContextMenu("CrossTest")]
    void crossTest()
    {
        Debug.Log("Cross = " + Vector3.Cross(startPos, endPos));

        //return new Vector3 (lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        var dot = startPos.x * endPos.x + startPos.y * endPos.y + startPos.z * endPos.z;
        Debug.Log("Cross = " + dot);
    }

    [ContextMenu("RotateVector")]
    void RotateVector()
    {
        var v = Math.RotateVector(endPos - startPos, rotVector);

        Debug.Log(v);
        Debug.DrawLine(startPos, endPos, Color.red, 1f);
        Debug.DrawLine(startPos, startPos + v, Color.green, 1f);
    }

    [ContextMenu("ShowRotateXZAround")]
    void ShowRotateXZAround()
    {
        if (!Application.isPlaying)
        {
            DLog.LogError("[ShowRotateXZAround] only can excute in playing");
            return;
        }

        IEnumerator rot()
        {
            Debug.Log("[ShowRotateXZAround] Start");
            Vector3 degreeV = Vector3.zero;

            while (degreeV.y < 360)
            {
                var v = Math.RotateVector(endPos - startPos, degreeV);
                                
                Debug.DrawLine(startPos, startPos + v, Color.green, 1f);

                degreeV.y += 2;
                yield return new WaitForFixedUpdate();
            }

            DLog.Log("[ShowRotateXZAround] Finish");
        }

        StartCoroutine(rot());
    }

    [ContextMenu("ShowRotateXYAround")]
    void ShowRotateXYAround()
    {
        if (!Application.isPlaying)
        {
            DLog.LogError("[ShowRotateXYAround] only can excute in playing");
            return;
        }

        IEnumerator rot()
        {
            Debug.Log("[ShowRotateXYAround] Start");
            Vector3 degreeV = Vector3.zero;

            while (degreeV.z < 360)
            {
                var v = Math.RotateVector(endPos - startPos, degreeV);

                //Debug.DrawLine(Vector3.zero, originVector, Color.red, 1f);
                Debug.DrawLine(startPos, startPos + v, Color.green, 1f);

                degreeV.z += 2;
                yield return new WaitForFixedUpdate();
            }

            DLog.Log("[ShowRotateXYAround] Finish");
        }

        StartCoroutine(rot());
    }
    [ContextMenu("ShowRotateYZAround")]
    void ShowRotateYZAround()
    {
        if (!Application.isPlaying)
        {
            DLog.LogError("[ShowRotateYZAround] only can excute in playing");
            return;
        }

        IEnumerator rot()
        {
            Debug.Log("[ShowRotateYZAround] Start");
            Vector3 degreeV = Vector3.zero;

            while (degreeV.x < 360)
            {
                var v = Math.RotateVector(endPos - startPos, degreeV);

                //Debug.DrawLine(Vector3.zero, originVector, Color.red, 1f);
                Debug.DrawLine(startPos, startPos + v, Color.green, 1f);

                degreeV.x += 2;
                yield return new WaitForFixedUpdate();
            }

            DLog.Log("[ShowRotateYZAround] Finish");
        }

        StartCoroutine(rot());
    }
}
