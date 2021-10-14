using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDraw : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject tempLinePrefab;

    //[Space(30f)]
    public Gradient lineColor, lineColor2;
    public float linePointMinDistance;
    public float lineWidth;

    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;//Gán Line vừa vẽ thành bị chặn

    Line currentLine;
    Line tempLine;

    public Vector2 mousePos;
    public Vector2 lastPoint;//Điểm đc vẽ cuối cùng trước khi bị chặn
    public bool beingBlocked;//Đang trong trạng thái bị chặn
    
    

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
            //Debug.Log("dang ve");
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }


        TestAll();
    }

    public void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointMinDistance(linePointMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    //Xác định con trỏ thoát khỏi bị chặn ở vị trí thỏa mãn để nối line không xuyên qua đối tượng chặn
    public void NotPenetrate()
    {
        //Method draw line đc kích hoạt khi thoát khỏi bị chặn(đã bị chặn trước đó)
        //if (beingBlocked == true)
        //{
        //    Vector2 blockedExitPos = cam.ScreenToWorldPoint(Input.mousePosition);

        //    float tempX = (blockedExitPos.x + lastPoint.x) / 2;
        //    float tempY = (blockedExitPos.y + lastPoint.y) / 2;

        //    Vector2 tempMousePos = new Vector2(tempX, tempY);
        //    testPosition.transform.position = tempMousePos;

        //    RaycastHit2D hit2 = Physics2D.CircleCast(tempMousePos, lineWidth / 3, Vector2.zero, 1f, cantDrawOverLayer);
        //    if (hit2)
        //    {
        //        Debug.Log("khong the xuyen qua");
        //    }
        //    else
        //    {
        //        currentLine.AddPoints(mousePos);

        //    }
        //}
        //else
        //{
        //    currentLine.AddPoints(mousePos);
        //}

        if (beingBlocked == true)
        {
            //////////
            tempLine = Instantiate(tempLinePrefab, lastPoint, this.transform.rotation).GetComponent<Line>();

            tempLine.UsePhysics(false);
            tempLine.SetLineColor(lineColor2);
            tempLine.SetPointMinDistance(linePointMinDistance);
            tempLine.SetLineWidth(lineWidth);

            tempLine.AddPoints(mousePos);

            beingBlocked = false;
        }

    }

    public void Draw()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (beingBlocked == true)
        {
            //Debug.Log(mousePos);
        }
        
        RaycastHit2D hit = Physics2D.CircleCast(mousePos, lineWidth / 3, Vector2.zero, 0.1f, cantDrawOverLayer);

        if (hit)
        {
            beingBlocked = true;
            lastPoint = currentLine.lineRenderer.GetPosition(currentLine.pointsCount - 1);
            //Debug.Log("bi chan vi tri: " + lastPoint);
        }
        else
        {
            NotPenetrate();
            //currentLine.AddPoints(mousePos);
            beingBlocked = false;
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
