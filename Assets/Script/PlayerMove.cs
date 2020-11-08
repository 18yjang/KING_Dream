using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Button Up, Down, Left, Right;
    public GameObject Player;
    public DrawLine drawLine;
    public RectTrans rectTrans;
    public string prevButton;
    public string[] usedButton;

    public int BtnIndex;
    //public Vector2 prevPosition;
    //public Vector2 newPosition;
    
    // public int rotation;
   // public int lineNum;

   

    void Start()
    {
        usedButton = new string[4];
        BtnIndex = 0;
        //prevPosition = Player.transform.position;
        //points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));
        //var line = Instantiate(Resources.Load("Prefab/Line"), new Vector2(Player.transform.position.x, Player.transform.position.y), Quaternion.identity);
    }

    
    void Update()
    {
        
    }

    public void OnTouchButton(string button)
    {
        if (BtnIndex == 4)          // 벽에 안닿고 5번째 버튼을 누르는 경우: 모든 좌표 초기화 (새로 네모 그려야함)
        {
            drawLine.line.positionCount = 0;
            drawLine.line.positionCount = 1;        // 좌표 초기화
            drawLine.pointNum = 0;
            BtnIndex = 0;           // 인덱스 초기화
            usedButton = null;
            usedButton = new string[4];         // 사용한 버튼 목록 초기화
        }

        for (int i = 0; i<BtnIndex; i++)        // 왜 안되는거지
        {
            if(button.Equals(usedButton[BtnIndex]))
            {  
                goto EXIT;                 // 이미 한번 사용한 경우 사용 불가
            }
        }
        

        Left.enabled = true;
        Right.enabled = true;
        Up.enabled = true;
        Down.enabled = true;            // 모든 버튼 사용 가능하게 함

        //if (prevButton == button) Debug.Log("same button"); for 루프에서 확인 가능
            /*                  안쓰는것                  */
            //prevPosition = newPosition;
            //newPosition = Player.transform.position;
            //points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));



            // 버튼 누르면 진행하던 방향 속도 및 힘 0으로 변경
            Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            drawLine.Draw();   // DrawLine 의 draw()   - LineRenderer 포지션 셋


            switch (button)
            {
                case "Left":
                Player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(-1, 0);
                    Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    drawLine.pointNum++;
                    drawLine.line.positionCount = drawLine.pointNum + 1;
                drawLine.line.SetPosition(drawLine.pointNum + 1, Player.transform.position);
                Left.enabled = false;
                    Right.enabled = false;
                    usedButton[BtnIndex] = "Left";
                    BtnIndex++;
                break;
                case "Right":
                    Player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                    Player.GetComponent<ConstantForce2D>().force = new Vector2(1, 0);
                    Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    drawLine.pointNum++;
                    drawLine.line.positionCount = drawLine.pointNum + 1;
                drawLine.line.SetPosition(drawLine.pointNum + 1, Player.transform.position);
                Left.enabled = false;
                    Right.enabled = false;
                    usedButton[BtnIndex] = "Right";
                    BtnIndex++;
                break;
                case "Up":
                Player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(0, 1);
                    Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    drawLine.pointNum++;
                    drawLine.line.positionCount = drawLine.pointNum + 1;
                drawLine.line.SetPosition(drawLine.pointNum + 1, Player.transform.position);
                Down.enabled = false;
                    Up.enabled = false;
                    usedButton[BtnIndex] = "Up";
                    BtnIndex++;
                break;
                case "Down":
                Player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(0, -1);
                    Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    drawLine.pointNum++;
                    drawLine.line.positionCount = drawLine.pointNum + 1;
                    drawLine.line.SetPosition(drawLine.pointNum+1, Player.transform.position);
                    Down.enabled = false;
                    Up.enabled = false;
                    usedButton[BtnIndex] = "Down";
                    BtnIndex++;
                break;
            }
        
    EXIT: return;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

            Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        drawLine.line.SetPosition(drawLine.pointNum-1, Player.transform.position);


        /*                  안쓰는것                  */
        //drawLine.colliderpoints.Add(prevPosition);
        //drawLine.lineCollider.points = drawLine.colliderpoints.ToArray();
        //prevPosition = Player.transform.position;
        //newPosition = Player.transform.position;


        if (drawLine.line.positionCount > 3)
        {

            if(collision.gameObject.tag == "Wall")
            {
                float[] x = new float[4], y = new float[4];

                for (int i = 0; i < 4; i++)
                {
                    drawLine.colliderpoints[i].x = x[i];
                    drawLine.colliderpoints[i].y = y[i];
                }

                float xCoor, yCoor, xLen, yLen, smallx, bigy;
                xCoor = Math.Abs(x[1] - x[3]);
                yCoor = Math.Abs(y[1] - y[3]);

                if (x[1] < x[3]) smallx = x[1]; else smallx = x[3];
                if (y[1] > y[3]) bigy = y[1]; else bigy = y[3];

                xLen = smallx + xCoor;
                yLen = bigy + yCoor;


                rectTrans.CreateImage(new Vector2(xCoor, yCoor), xLen, yLen);

                //rectTrans.CreateImage();
                /*************************CreateImage*********************************/
                // 좌표계 4개, 각 x와 y 비교.
                // x랑 y좌표 모두(&&) 틀린경우, 그 두 점 저장.
                // 모든 Image의 좌표 위치 List로 저장해두기.
                // 임의의 6개 좌표 저장하기. (x,y) 6세트.
                // Image의 위치 + 높이 혹은 넓이/2 가 좌표 점과 동일하면, Image의 위치 + 넓이 혹은 높이/2 를 뺀 값 * 주어진 좌표들의 값 = 넓이.
                //rectTrans.CreateImage();

                //drawLine.colliderpoints.Clear();
            }

            drawLine.line.positionCount = 0;
            drawLine.line.positionCount = 1;

            Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            //drawLine.colliderpoints.Add(prevPosition);
            //drawLine.lineCollider.points = drawLine.colliderpoints.ToArray();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(drawLine.line.positionCount%4 == 0) {
            float[] x = new float[4], y = new float[4];

            for(int i = 0; i<4; i++)
            {
                drawLine.colliderpoints[i].x = x[i];
                drawLine.colliderpoints[i].y = y[i];
            }

            float xCoor, yCoor, xLen, yLen, smallx, bigy;
            xCoor = Math.Abs(x[1] - x[3]);
            yCoor = Math.Abs(y[1] - y[3]);

            if (x[1] < x[3]) smallx = x[1]; else smallx = x[3];
            if (y[1] > y[3]) bigy = y[1]; else bigy = y[3];

            xLen = smallx + xCoor;
            yLen = bigy + yCoor; 
            

            rectTrans.CreateImage(new Vector2(xCoor,yCoor), xLen, yLen);

            /****************************************CreateImage*********************************/
            // 충돌좌표(ex. 6)+ 5, 4, 3 좌표 4개로 박스 넣기. 좌표는(x+(x1-x2)/2 , y+(y1-y2)/2)
            // Instantiate(Resources.Load("Prefab/Image")) - image instantiate
            // transform.parent = "Canvas"
            // transform.scale = new Vector2(x좌표 빼고/2+x, 좌표 빼고/2+X)     혹은 width랑 height 설정하기. (좌표들 뺀값만)
            // boxCollider2D.transform.size = RectTransform.width & Height



            /*                  안쓰는것                  */
            //prevPosition = Vector2.zero;
            //newPosition = Vector2.zero;
        }
    }
}
