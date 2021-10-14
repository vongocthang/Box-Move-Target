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
    int cantDrawOverLayerIndex;//Gán Line vừa vẽ thành bị chặn

    Line currentLine;

    public Vector2 mousePos;
    public Vector2 lastPoint;
    public bool drawBlocked;
    //public Vector2 centerPos;//Điểm chính giữa của điểm bị chặn và điểm thoát ra
    //public Vector2 mousePos2;

    Camera cam;

    public GameObject testPosition;

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
            Debug.Log("dang ve");
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

    public void NotPenetrate()
    {
        //Method draw line đc kích hoạt khi thoát khỏi bị chặn (đã bị chặn trước đó)
        //if (drawBlocked == true)
        //{
        //    //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //    //Điều kiện để Line đc nối lại - Line k đc đi xuyên qua đối tượng Blocked
        //    if (Mathf.Abs(mousePos.x - lastPoint.x) <= differenceDistance || Mathf.Abs(mousePos.y - lastPoint.y) <= differenceDistance)
        //    {
        //        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //        float tempX = (mousePos.x + lastPoint.x) / 2;
        //        float tempY = (mousePos.y + lastPoint.y) / 2;

        //        Vector2 tempMousePos = new Vector2(tempX, tempY);
        //        testPosition.transform.position = tempMousePos;

        //        RaycastHit2D hit2 = Physics2D.CircleCast(tempMousePos, lineWidth / 3, Vector2.zero, 1f, cantDrawOverLayer);
        //        if (hit2)
        //        {
        //            drawBlocked = true;
        //            Debug.Log("khong the xuyen qua");
        //        }
        //        else
        //        {
        //            currentLine.AddPoints(mousePos);
        //        }
        //    }
        //}

        //Method draw line đc kích hoạt khi thoát khỏi bị chặn(đã bị chặn trước đó)
        if (drawBlocked == true)
        {
            float tempX = (mousePos.x + lastPoint.x) / 2;
            float tempY = (mousePos.y + lastPoint.y) / 2;

            Vector2 tempMousePos = new Vector2(tempX, tempY);
            testPosition.transform.position = tempMousePos;

            RaycastHit2D hit2 = Physics2D.CircleCast(tempMousePos, lineWidth / 3, Vector2.zero, 1f, cantDrawOverLayer);
            if (hit2)
            {
                Debug.Log("khong the xuyen qua");
            }
            else
            {
                currentLine.AddPoints(mousePos);
                drawBlocked = false;
            }
        }

        else
        {
            currentLine.AddPoints(mousePos);
        }

    }

    public void Draw()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        

        RaycastHit2D hit = Physics2D.CircleCast(mousePos, lineWidth / 3, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit)
        {
            drawBlocked = true;
            lastPoint = currentLine.lineRenderer.GetPosition(currentLine.pointsCount - 1);
            Debug.Log("bi chan vi tri: " + lastPoint);
        }
        else
        {
            NotPenetrate();
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
                //currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
    }
}
