using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransform : MonoBehaviour
{
    Transform vec;
   
    void Start()
    {
       // vec = new Vector2(0,0);
        CreateBox();
    }

    
    void Update()
    {
        //CreateBox();
    }

    public void CreateBox()
    {
        //GameObject box;
        var box = Instantiate(Resources.Load("Prefab/Box"), new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        box.name = this.name;
        //box.transform.position = new Vector3(0, 0, 0);

    }

    /*private Vector3 Vector3(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }*/
}
