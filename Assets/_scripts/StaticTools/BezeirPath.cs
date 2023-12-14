using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezeirPath
{

    public static List<Vector3> GetBezierPath(Vector3 fromVector, Vector3 handle1, Vector3 handle2, Vector3 toVector, float jumpRatio=0.14f)
    {
        var pointList = new List<Vector3>();

        for (var ratio = 0f; ratio <= 1+jumpRatio; ratio += jumpRatio)
        {
            var curve = cubicLerp(fromVector, handle1, handle2, toVector, ratio);
            pointList.Add(curve);
        }
 
        return pointList;
    }

    static Vector3 quadLerp(Vector3 fromVector, Vector3 handle, Vector3 toVector, float ratio)
    {
        var ab = Vector3.Lerp(fromVector, handle, ratio);
        var bc = Vector3.Lerp(handle, toVector, ratio);

        return Vector3.Lerp(ab, bc, ratio);
    }
    static Vector3 cubicLerp(Vector3 fromVector, Vector3 handle1, Vector3 handle2, Vector3 toVector, float ratio)
    {
        var abc = quadLerp(fromVector, handle1, handle2, ratio);
        var bcd = quadLerp(handle1, handle2, toVector, ratio);

        return Vector3.Lerp(abc, bcd, ratio);
    }
     
}
