using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject penSprite;
    public int countLine;

    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    public Line line;
    public CircleCollider2D circleCollider;

    Camera cam;

    //public GameObject pen;
    public float penMoveSpeed;
    public Vector2 penPosition;
    public Vector2 mousePosition;
    public GameObject penCollider;

    public LayerMask blockLayer;//Dùng khi bắt đầu vẽ
    int blockLayerIndex;
    public bool drawing;//Đang vẽ
    public bool blocked;

    //Các đối tượng cần tắt tác dụng lực khi đang vẽ
    public Rigidbody2D car;
    public Rigidbody2D barrierORbox;
    public Rigidbody2D wheel1, wheel2;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        //blockLayerIndex = LayerMask.NameToLayer("CantDrawOver");

        Time.timeScale = 100;
    }

    // Update is called once per frame
    void Update()
    {
        BeforeDraw();

        if (Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
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
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 2f, Vector2.zero, 1f, blockLayer);
        if (hit)
        {
            Debug.Log("Layer này không thể vẽ lên");
        }
        else
        {
            line = Instantiate(linePrefab, new Vector2(0, 0), this.transform.rotation).GetComponent<Line>();
            line.UsePhysics(false);
            line.SetLineColor(lineColor);
            line.SetPointMinDistance(linePointsMinDistance);
            line.SetLineWidth(lineWidth);

            penSprite.SetActive(true);
        }
    }

    public void Draw()
    {
        penPosition = this.transform.position;
        
        RaycastHit2D hit = Physics2D.CircleCast(penPosition, lineWidth / 2f, Vector2.zero, 1f, blockLayer);
        if (hit)
        {
            
        }
        else
        {

            circleCollider.isTrigger = false;
            line.AddPoint(penPosition);
        }
    }

    public void EndDraw()
    {
        if (line != null)
        {
            if (line.pointsCount < 2)
            {
                
            }
            else
            {
                
            }

            line.UsePhysics(true);
            countLine++;
            line = null;

            penSprite.SetActive(false);
        }

        blocked = false;
        drawing = false;

        circleCollider.isTrigger = true;
        this.transform.position = new Vector2(0, 10);

        //Bật lại tác dụng lực khi vẽ xong
        //car.isKinematic = false;
        //barrierORbox.isKinematic = false;
        //wheel1.isKinematic = false;
        //wheel2.isKinematic = false;
    }

    public void SetUpPen()
    {
        this.transform.position = mousePosition;
    }

    public void PenFollowMouse()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, mousePosition, penMoveSpeed*Time.deltaTime);
    }

    public void PenCollider()
    {
        penCollider.transform.position = this.transform.position;
    }

    public void BeforeDraw()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        penPosition = this.transform.position;
    }
}
