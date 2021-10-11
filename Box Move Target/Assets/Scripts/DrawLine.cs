using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rb2D;

    public List<Vector2> points = new List<Vector2>();
    public int pointsCount = 0;

    float pointsMinDistance = 0.1f;

    public void AddPoints()
    {

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

        edgeCollider.edgeRadius = width / 2f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
