using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MatrixBlender : MonoBehaviour
{
    public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
    {
        Matrix4x4 ret = new Matrix4x4();
        for (int i = 0; i < 16; i++)
            ret[i] = Mathf.Lerp(from[i], to[i], time);
        return ret;
    }

    private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration)
    {
        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            Camera.main.projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);

            
            yield return 1;
        }
        Camera.main.projectionMatrix = dest;
    }

    public Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration)
    {
        StopAllCoroutines();
        return StartCoroutine(LerpFromTo(Camera.main.projectionMatrix, targetMatrix, duration));
    }

// Function for EaseOutCubic in algebraic terms, 
// Needs to be ported to C# since this is actionscript code...
//
//    Math.easeOutCubic = function (t, b, c, d) {
//    t /= d;
//    t--;
//    return c*(t*t*t + 1) + b;
//};
}