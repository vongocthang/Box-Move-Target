using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;

    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    Line line;

    Camera cam;

    //public GameObject pen;
    public float penMoveSpeed;
    public Vector2 penPosition;
    public Vector2 mousePosition;
    public GameObject penCollider;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            SetUpPen();
            BeginDraw();
        }
        if (Input.GetMouseButton(0))
        {
            PenFollowMouse();
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }

        PenCollider();
    }

    public void BeginDraw()
    {
        line = Instantiate(linePrefab, new Vector2(0, 0), this.transform.rotation).GetComponent<Line>();

        line.UsePhysics(false);
        line.SetLineColor(lineColor);
        line.SetPointMinDistance(linePointsMinDistance);
        line.SetLineWidth(lineWidth);

        line.SetEdgeCollider(true);
    }

    public void Draw()
    {
        line.AddPoint(penPosition);
    }

    public void EndDraw()
    {
        line.SetCircleColloder(false);
        line.SetEdgeCollider(false);

        line.UsePhysics(true);
        line = null;
    }

    public void SetUpPen()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        this.transform.position = mousePosition;
    }

    public void PenFollowMouse()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = Vector2.Lerp(this.transform.position, mousePosition, penMoveSpeed*Time.deltaTime);

        penPosition = this.transform.position;
    }

    public void PenCollider()
    {
        penCollider.transform.position = this.transform.position;
    }
}
