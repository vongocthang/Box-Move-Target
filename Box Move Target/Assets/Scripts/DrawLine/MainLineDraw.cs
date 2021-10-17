using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLineDraw : MonoBehaviour
{
    public GameObject linePrefab;

    public Gradient lineColor;
    public float lineWidth;

    public LayerMask cantDrawOverLayer;

    MainLine mainLine;

    public TempLineDraw tempLineDraw;

    public Vector2 mousePos;
    public RaycastHit2D rcHit;
    public bool blocked = false;

    Camera cam;

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
        if (mainLine != null)
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
        mainLine = Instantiate(linePrefab, this.transform).GetComponent<MainLine>();

        mainLine.UsePhysics(false);
        mainLine.SetLineColor(lineColor);
        mainLine.SetLineWidth(lineWidth);
    }

    public void Draw()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        rcHit = Physics2D.CircleCast(mousePos, lineWidth / 2f, Vector2.zero, 1f, cantDrawOverLayer);

        if (rcHit)
        {
            if (blocked == true)
            {

            }
            else
            {
                Debug.Log("Bắt đầu bị chặn");

                blocked = true;

                tempLineDraw.BeginDraw(mousePos);
                tempLineDraw.Draw(mainLine.GetLastPoint());
            }
        }
        else
        {
            if (blocked == true)
            {
                tempLineDraw.Draw(mousePos);

                if (tempLineDraw.tempLine.blocked == true)
                {

                }
                else
                {
                    Debug.Log("Thoát khỏi bị chặn");

                    mainLine.AddPoints(mousePos);
                    blocked = false;

                    tempLineDraw.EndDraw();
                }
            }
            else
            {
                Debug.Log("Không bị chặn");

                mainLine.AddPoints(mousePos);
            }
        }
    }

    public void EndDraw()
    {
        if (mainLine != null)
        {
            if (mainLine.lineRenderer.positionCount == 1)
            {
                Debug.Log("Kết thúc vẽ 1 điểm");

                
                
            }
            else
            {
                Debug.Log("Kết thúc vẽ nhiều hơn 1 điểm");

               
            }

            mainLine.UsePhysics(true);
            mainLine = null;
            blocked = false;

            tempLineDraw.EndDraw();
        }
    }
}
