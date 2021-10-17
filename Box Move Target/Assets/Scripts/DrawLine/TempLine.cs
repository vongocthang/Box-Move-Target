using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rb2D;
    //public Vector2 newPoint;

    public int pointsCount = 0;

    //float circleColliderRadius;
    public EdgeCollider2D edgeCollider;
    float edgeColliderRadius;
    public List<Vector2> points = new List<Vector2>();

    public bool blocked;//Line vẽ tạm thời bị chặn

    public void AddPoints(Vector2 newPoint)
    {
        pointsCount++;
        points.Add(newPoint);

        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.edgeRadius = edgeColliderRadius;
        if (pointsCount >= 1)
        {
            edgeCollider.points = points.ToArray();
        }

        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);
    }

    public void UsePhysics(bool usePhysics)
    {
        rb2D.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient gradientColor)
    {
        lineRenderer.colorGradient = gradientColor;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        edgeColliderRadius = width / 2f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Box" || collision.tag == "Tilemap" || collision.tag == "Car")
        {
            Debug.Log("Va chạm với " + collision.gameObject.tag);
            blocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Box" || collision.tag == "Tilemap" || collision.tag == "Car")
        {
            Debug.Log("Thoát ra khỏi " + collision.gameObject.tag);
            blocked = false;
        }
    }
}
