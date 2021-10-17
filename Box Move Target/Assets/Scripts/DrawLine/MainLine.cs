using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D rb2D;

    public int pointsCount = 0;

    public CircleCollider2D circleCollider;
    float circleColliderRadius;
    public EdgeCollider2D edgeCollider;
    float edgeColliderRadius;
    public List<Vector2> points = new List<Vector2>();

    public void AddPoints(Vector2 newPoint)
    {
        if (Vector2.Distance(newPoint, GetLastPoint()) >= 0.1f)
        {
            pointsCount++;
            points.Add(newPoint);

            circleCollider = gameObject.AddComponent<CircleCollider2D>();
            circleCollider.offset = newPoint;
            circleCollider.radius = circleColliderRadius;

            edgeCollider = GetComponent<EdgeCollider2D>();
            edgeCollider.edgeRadius = edgeColliderRadius;
            if (pointsCount >= 1)
            {
                edgeCollider.points = points.ToArray();
                edgeCollider.enabled = true;
            }
            else
            {
                Destroy(edgeCollider);
            }

            lineRenderer.positionCount = pointsCount;
            lineRenderer.SetPosition(pointsCount - 1, newPoint);
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

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;
        edgeColliderRadius = width / 2f;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.tag=="Box" || collision.tag == "Tilemap" || collision.tag == "Car")
    //    {
    //        Debug.Log("Va chạm với " + collision.gameObject.tag);
    //        blocked = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Box" || collision.tag == "Tilemap" || collision.tag == "Car")
    //    {
    //        Debug.Log("Thoát ra khỏi " + collision.gameObject.tag);
    //        blocked = false;
    //    }
    //}
}
