using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    public GameObject linePrefab;

    //[Space(30f)]
    public Gradient lineColor;
    public float linePointMinDistance;
    public float lineWidth;

    Line currentLine;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            BeginDraw();
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    BeginDraw();
        //}
        if (currentLine != null)
        {
            Draw();
        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    EndDraw();
        //}
        if (Input.touchCount == 0)
        {
            EndDraw();
        }
        
    }

    public void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointMinDistance(linePointMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    public void Draw()
    {
        Vector2 touchPos = cam.ScreenToWorldPoint(Input.GetTouch(0).position);
        currentLine.AddPoints(touchPos);
        //Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //currentLine.AddPoints(mousePos);
    }

    public void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                //Nét vẽ không thể là một điểm
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}
