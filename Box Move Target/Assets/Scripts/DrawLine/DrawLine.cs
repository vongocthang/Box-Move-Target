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
    public Rigidbody2D barrier;
    public Rigidbody2D wheel1, wheel2;
    //public Rigidbody2D box;
    public Rigidbody2D wood1, wood2, wood3, wood4, wood5;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        blockLayerIndex = LayerMask.NameToLayer("CantDrawOver");

        Time.timeScale = 5;
    }

    // Update is called once per frame
    void Update()
    {
        BeforeDraw();

        if (drawing == false)
        {
            SetUpPen();
        }

        if (Input.GetMouseButtonDown(0))
        {
            //SetUpPen();
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

        Time.timeScale = 1;
    }

    public void Draw()
    {
        RaycastHit2D hit = Physics2D.CircleCast(penPosition, lineWidth / 2, Vector2.zero, 1f, blockLayer);
        if (hit && drawing == false)
        {
            Debug.Log("Bi chan");
            blocked = true;
        }
        else
        {
            //Tắt tác dụng lực khi đang vẽ
            car.isKinematic = true;
            
            barrier.isKinematic = true;
            wheel1.isKinematic = true;
            wheel2.isKinematic = true;
            //wood1.isKinematic = true;
            //wood2.isKinematic = true;
            //wood3.isKinematic = true;
            //wood4.isKinematic = true;
            //wood5.isKinematic = true;

            drawing = true;
            line.AddPoint(penPosition);
            circleCollider.isTrigger = false;


        }
    }

    public void EndDraw()
    {
        if (blocked == true)
        {
            Destroy(line.gameObject);
        }

        //Bật lại tác dụng lực khi vẽ xong
        car.isKinematic = false;
        barrier.isKinematic = false;
        wheel1.isKinematic = false;
        wheel2.isKinematic = false;
        //wood1.isKinematic = false;
        //wood2.isKinematic = false;
        //wood3.isKinematic = false;
        //wood4.isKinematic = false;
        //wood5.isKinematic = false;

        line.UsePhysics(true);
        line = null;

        circleCollider.isTrigger = true;
        drawing = false;
        blocked = false;
        this.transform.position = new Vector2(0, 10);


    }

    public void SetUpPen()
    {
        this.transform.position = mousePosition;
    }

    public void PenFollowMouse()
    {
        this.transform.position = Vector2.Lerp(this.transform.position, mousePosition, penMoveSpeed*Time.deltaTime);
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
