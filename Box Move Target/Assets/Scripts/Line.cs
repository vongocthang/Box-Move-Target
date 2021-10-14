using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rb2D;

    public List<Vector2> points = new List<Vector2>();
    public int pointsCount = 0;

    float pointsMinDistance = 0.1f;

    public CircleCollider2D circleCollider;
    float circleColliderRadius;

    public void AddPoints(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
        {
            return;
        }

        points.Add(newPoint);
        pointsCount++;

        circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;

        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
        //Debug.Log(lineRenderer.positionCount);

        if (pointsCount > 1)
        {
            edgeCollider.points = points.ToArray();
        }
    }

    public Vector2 GetLastPoint()
    {
        return (Vector2) lineRenderer.GetPosition(pointsCount - 1);
    }

    public void UsePhysics(bool usePhysics)
    {
        rb2D.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient gradientColor)
    {
        lineRenderer.colorGradient = gradientColor;
    }

    public void SetPointMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;

        edgeCollider.edgeRadius = width / 2f;
    }
}
