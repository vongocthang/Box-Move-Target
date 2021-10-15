using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject tempLinePrefab;

    //[Space(30f)]
    public Gradient lineColor, lineColor2;
    public float lineWidth;

    public LayerMask cantDrawOverLayer;

    Line currentLine;
    Line tempLine;

    public Vector2 mousePos;
    public RaycastHit2D rcHit;
    public bool startBlocked = false;
    public Vector2 startBlockedPos;
    public bool endBlocked = false;
    public Vector2 endBlockedPos;

    public GameObject testPos;

    Camera cam;

    public List<Vector2> points = new List<Vector2>();
    public EdgeCollider2D edgeCollider;

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
            BeginDraw();
        }
        if (currentLine != null)
        {
            Draw();
            //Debug.Log("dang ve");
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
        currentLine.SetLineWidth(lineWidth);
    }

    public void BeginDrawTemp()
    {
        tempLine = Instantiate(tempLinePrefab, this.transform).GetComponent<Line>();

        tempLine.UsePhysics(false);
        tempLine.SetLineColor(lineColor2);
        tempLine.SetLineWidth(lineWidth);
    }

    public void Draw()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        currentLine.newPoint = mousePos;
        
        

        rcHit = Physics2D.CircleCast(mousePos, lineWidth / 3, Vector2.zero, 1f, cantDrawOverLayer);

        if (rcHit)
        {
            if (startBlocked == false)
            {

                startBlockedPos = currentLine.lineRenderer.GetPosition(currentLine.lineRenderer.positionCount - 1);
                points.Add(startBlockedPos);
                tempLine = Instantiate(tempLinePrefab, this.transform).GetComponent<Line>();
                tempLine.SetLineWidth(0.2f);
                tempLine.lineRenderer.SetPosition(0, startBlockedPos);
                tempLine.UsePhysics(false);
                startBlocked = true;
            }
            //checkStartBlocked++;
        }
        else
        {
            if (startBlocked == false)
            {
                currentLine.AddPoints();
            }
            else
            {
                if (startBlocked == true)
                {
                    endBlocked = true;
                    endBlockedPos = mousePos;
                    points.Add(mousePos);
                    tempLine.lineRenderer.SetPosition(1, endBlockedPos);
                    //Destroy(tempLine.gameObject.GetComponent<EdgeCollider2D>());
                    //tempLine.gameObject.AddComponent<EdgeCollider2D>();
                    tempLine.GetComponent<EdgeCollider2D>().points = points.ToArray();
                    //tempLine.GetComponent<EdgeCollider2D>().isTrigger = true;

                    if (tempLine.blocked == false)
                    {
                        currentLine.AddPoints();

                        tempLine = null;
                        startBlocked = false;
                        endBlocked = false;
                    }


                    //if (tempLine.blocked == true)
                    //{
                    //    Destroy(tempLine.gameObject);
                    //}
                }
                else
                {

                }

                //endBlockedPos = mousePos;

                //tempLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
                //tempLine.lineRenderer.SetPosition(0, startBlockedPos);
                //tempLine.lineRenderer.SetPosition(1, endBlockedPos);
                //tempLine.edgeCollider = tempLine.gameObject.AddComponent<EdgeCollider2D>();
                //tempLine.edgeCollider.isTrigger = true;
            }
        }
        //checkStartBlocked = 0;
    }

    public void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.lineRenderer.positionCount == 1)
            {
                currentLine.pointsCount++;
                currentLine.lineRenderer.positionCount = currentLine.pointsCount;
                currentLine.lineRenderer.SetPosition(currentLine.pointsCount - 1, currentLine.newPoint);

                currentLine.UsePhysics(true);
                currentLine = null;
            }
            else
            {
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }

    public void DrawTempLine()
    {
        
        
    }

    public void TestAll()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Xoa diem ve cuoi");
            currentLine.lineRenderer.positionCount -= 2;
        }
    }


}
