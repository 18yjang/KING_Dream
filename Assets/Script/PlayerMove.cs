using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Button Up, Down, Left, Right, Go, Stop;
    public GameObject Player;
    // public int rotation;
    //public int pointNum;
   // public int lineNum;

    List<Vector2> points = new List<Vector2>();

    void Start()
    {
        points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));
    }

    
    void Update()
    {
        
    }

    public void OnTouchButton(string button)
    {
        switch (button)
        {
            case "Left":
                Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(-1, 0);
                break;
            case "Right":
                Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(1, 0);
                break;
            case "Up":
                Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(0, 1);
                break;
            case "Down":
                Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                points.Add(new Vector2(Player.transform.position.x, Player.transform.position.y));
                Player.GetComponent<ConstantForce2D>().force = new Vector2(0, -1);
                break;


            /*case "Go":
                if (rotation == 1) Player.GetComponent<ConstantForce2D>().force = new Vector2(-1, 0);
                else if (rotation == 2) Player.GetComponent<ConstantForce2D>().force = new Vector2(1, 0);
                else if (rotation == 4) Player.GetComponent<ConstantForce2D>().force = new Vector2(0, -1);
                else if (rotation == 3) Player.GetComponent<ConstantForce2D>().force = new Vector2(0, 1);
                break;
            case "Stop":
                Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;*/
        }
    }
}
