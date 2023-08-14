using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierMovement : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;

    [Range(0, 1)] public float t;
    void Update()
    {
        transform.position = Bezier.GetPoint(p0.position, p1.position, p2.position, t);
        transform.rotation =
            Quaternion.LookRotation(Bezier.GetPointDerivative(p0.position, p1.position, p2.position, t));
    }

    public void DrawLine()
    {
        var pointList = new List<Vector3>();
        var line = p0.gameObject.GetComponent<LineRenderer>();
        const int segmentsNumber = 20;
        
        for (int i = 0; i < segmentsNumber + 1; i++)
        {
            float param = (float)i / segmentsNumber;
            Vector3 point = Bezier.GetPoint(p0.position, p1.position, p2.position, param);
            pointList.Add(point);
        }
        line.SetPositions(pointList.ToArray());
    }

    private void OnDrawGizmos()
    {
        const int segmentsNumber = 20;
        Vector3 previousPoint = p0.position;
        
        for (int i = 0; i < segmentsNumber + 1; i++)
        {
            float param = (float)i / segmentsNumber;
            Vector3 point = Bezier.GetPoint(p0.position, p1.position, p2.position, param);
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}