using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTrans : MonoBehaviour
{
    Transform vec;
    DrawLine drawLine;
    public GameObject Image;

    void Start()
    {
        Image = Resources.Load("Prefab/Image") as GameObject;
    }

    
    void Update()
    {
        //CreateBox();
    }

    public void CreateImage(Vector2 location, float xLen, float yLen)
    {
        
        //GameObject img = Instantiate(Image);
        Instantiate(Image, location, Quaternion.identity);
        
        //img.transform.localPosition = location;
        //img.GetComponent<Transform>().SetParent(GameObject.Find("Hidden").GetComponent<Transform>());
        Image.GetComponent<Transform>().localScale = new Vector2(xLen, yLen);


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
