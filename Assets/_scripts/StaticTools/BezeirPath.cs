using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezeirPath
{

    public static List<Vector3> s_GetBezierPath(Vector3 _fromVector, Vector3 _handle1, Vector3 _handle2, Vector3 _toVector, float _jumpRatio=0.14f)
    {
        List<Vector3> _pointList = new List<Vector3>();

        for (float _ratio = 0; _ratio <= 1+_jumpRatio; _ratio += _jumpRatio)
        {
            Vector3 _curve = s_cubicLerp(_fromVector, _handle1, _handle2, _toVector, _ratio);
            _pointList.Add(_curve);
        }
 
        return _pointList;
    }

    static Vector3 s_quadLerp(Vector3 _fromVector, Vector3 _handle, Vector3 _toVector, float _ratio)
    {
        Vector3 ab = Vector3.Lerp(_fromVector, _handle, _ratio);
        Vector3 bc = Vector3.Lerp(_handle, _toVector, _ratio);

        return Vector3.Lerp(ab, bc, _ratio);
    }
    static Vector3 s_cubicLerp(Vector3 _fromVector, Vector3 _handle1, Vector3 _handle2, Vector3 _toVector, float _ratio)
    {
        Vector3 abc = s_quadLerp(_fromVector, _handle1, _handle2, _ratio);
        Vector3 bcd = s_quadLerp(_handle1, _handle2, _toVector, _ratio);

        return Vector3.Lerp(abc, bcd, _ratio);
    }
     
}
