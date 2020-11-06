using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public Button Up, Down, Left, Right, Go, Stop;
    public GameObject Player;
    public int rotation;

    void Start()
    {
        
    }

    
    void Update()
    {
        //Debug.Log(rotation);
    }

    public void OnTouchButton(string button)
    {
        switch (button)
        {
            case "Left": rotation = 1; Debug.Log(rotation);
                break;
            case "Right": rotation = 2; Debug.Log(rotation);
                break;
            case "Up": rotation = 3; Debug.Log(rotation);
                break;
            case "Down": rotation = 4; Debug.Log(rotation);
                break;
            case "Go":
                Debug.Log(rotation);
                if (rotation == 1) Player.GetComponent<ConstantForce2D>().force = new Vector2(-1, 0);
                else if (rotation == 2) Player.GetComponent<ConstantForce2D>().force = new Vector2(1, 0);
                else if (rotation == 4) Player.GetComponent<ConstantForce2D>().force = new Vector2(0, -1);
                else if (rotation == 3) Player.GetComponent<ConstantForce2D>().force = new Vector2(0, 1);
                break;
            case "Stop":
                Player.GetComponent<ConstantForce2D>().force = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
        }
    }
}
