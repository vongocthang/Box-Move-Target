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

    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;

    Line currentLine;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginDraw();
        }
        if (currentLine != null)
        {
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
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
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.CircleCast(mousePos, lineWidth / 3, Vector2.zero, 1f, cantDrawOverLayer);
        if (hit)
        {
            //EndDraw();
        }
        else
        {
            currentLine.AddPoints(mousePos);
        }
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
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}
