using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject Player;
    PlayerMove playerMove;
    public int pointNum;
    public LineRenderer line;
    public EdgeCollider2D lineCollider;
    //public List<Vector2> colliderpoints = new List<Vector2>();
    public Vector2[] colliderPoint = new Vector2[4];
    public Vector2 prevPosition;


    // 모든 좌표(4개씩 묶음) 저장해두기. 



    void Start()
    {
        pointNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int index = 0; index < colliderpoints.Count; index++)
        {
            lineCollider.points[index] = colliderpoints[index - 1];
        }*/
    }

    public void Draw()
    {
        line.positionCount = pointNum;

        Vector2[] colliderpoints;
        colliderpoints = lineCollider.points;
        colliderpoints[pointNum] =new Vector2((float)(Player.transform.position.x+0.1), (float)(Player.transform.position.y+0.1));
        lineCollider.points = colliderpoints;

        Debug.Log("I'm here!");
        //if (pointNum>0) lineCollider.points[pointNum-1] = colliderpoints[pointNum];

        line.SetPosition(pointNum, Player.transform.position);
           // colliderpoints.Add(Player.transform.position);
            
      
        
        //pointNum++;
    }
}
