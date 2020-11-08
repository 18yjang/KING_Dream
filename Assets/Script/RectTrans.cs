using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTrans : MonoBehaviour
{
    Transform vec;
    DrawLine drawLine;

    void Start()
    {
       // vec = new Vector2(0,0);
    }

    
    void Update()
    {
        //CreateBox();
    }

    public void CreateImage()
    {
        GameObject img = Instantiate((GameObject)Resources.Load("Prefab/Image"));
        img.transform.localPosition = new Vector2(300, 500);
        img.GetComponent<Transform>().SetParent(GameObject.Find("Canvas/BackGround").GetComponent<Transform>());
        img.GetComponent<Transform>().localScale = new Vector2(50, 50);


        //GameObject box = Instantiate(Resources.Load("Prefab/Image"), drawLine.colliderpoints[2], Quaternion.identity) as GameObject;
        Debug.Log("Im here!");
        //box.transform.parent = GameObject.Find("Canvas").transform;

        //box.name = this.name;
        //box.transform.position = (0, 0, 0);
        //n++;
    }

    /*private Vector3 Vector3(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }*/
}
