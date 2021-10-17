using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLineDraw : MonoBehaviour
{
    public GameObject tempLinePrefab;
    public TempLine tempLine;
    public MainLineDraw mainLineDraw;

    public Gradient lineColor;
    float lineWidth;

    // Start is called before the first frame update
    void Start()
    {
        lineWidth = mainLineDraw.lineWidth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginDraw(Vector2 beginPos)
    {
        tempLine = Instantiate(tempLinePrefab, this.transform).GetComponent<TempLine>();

        tempLine.SetLineColor(lineColor);
        tempLine.SetLineWidth(lineWidth);
        //tempLine.gameObject.transform.position = beginPos;
    }

    public void Draw(Vector2 endPoint)
    {
        tempLine.AddPoints(endPoint);
    }

    public void EndDraw()
    {
        Debug.Log("Kết thúc vẽ Line tạm");

        Destroy(tempLine.gameObject);
        tempLine = null;
    }
}
