using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
            2 * t * oneMinusT * p1 +
            t * t * p2;
    }
    
    public static Vector3 GetPointDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            2 * oneMinusT * (p1 - p0) +
            2 * t * (p2 - p1);
    }
}
