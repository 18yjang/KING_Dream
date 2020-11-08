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
    public Vector2[] colliderpoints;

    public Vector2 prevPosition;


    // 모든 좌표(4개씩 묶음) 저장해두기. 



    void Start()
    {
        pointNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        line.positionCount = pointNum+1;
        line.SetPosition(pointNum-1, Player.transform.position);

        
        colliderpoints = lineCollider.points;
        for (int i = pointNum%4; i < 4; i++)
            colliderpoints[i] =new Vector2((float)(Player.transform.position.x+0.1), (float)(Player.transform.position.y+0.1));
        lineCollider.points = colliderpoints;

        line.SetPosition(pointNum, Player.transform.position);
        
       
    }
}
