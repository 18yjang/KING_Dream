﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int starPoint;
    public GameObject star1;
    public GameObject star2;
    public Text ScriptTxt;


    void Start()
    {
        starPoint = 0;
        int RandomX = Random.Range(-544, 582);
        int RandomY = Random.Range(-293, 293);
        Debug.Log(RandomX + " " + RandomY);
        Debug.Log(RandomX + " " + RandomY);
        star1.transform.localPosition = new Vector2(RandomX, RandomY);
        Debug.Log(RandomX + " " + RandomY);
        RandomX = Random.Range(-544, 582);
        RandomY = Random.Range(-293, 293);

        while (true)
        {
            if (star1.transform.position.x - RandomX < 20)
            {
                RandomX = Random.Range(-544,582);
            }
            else if (star1.transform.position.y - RandomY < 5)
            {
                RandomY = Random.Range(-293, 293);
            }
            else break;
        }

       star1.GetComponent<Transform>().SetParent(GameObject.Find("BackGround").GetComponent<Transform>());
       star2.GetComponent<Transform>().SetParent(GameObject.Find("BackGround").GetComponent<Transform>());
        star2.transform.localPosition = new Vector2(RandomX, RandomY);
        star1.GetComponent<Transform>().localScale = new Vector2(50,50);
        star2.GetComponent<Transform>().localScale = new Vector2(50, 50);
    }

    
    void Update()
    {
        if(starPoint == 2)
        {
            SceneManager.LoadScene("HiddenPictureScene");
        }
    }

    public void OnStarTouch(int starNum)
    {
        starPoint++;
        ScriptTxt.GetComponent<Text>().text = starPoint.ToString();
        if (starNum == 1)
            star1.SetActive(false);
        else
            star2.SetActive(false);




    }

    public void OnClickButton(string where)
    {
        if (where.Equals("return"))
        {
            Debug.Log("메인");
            // 메인화면으로
            HiddenPicture.isBefore = true;
            SceneManager.LoadScene("HiddenPictureScene");
        }
        else if (where.Equals("continue"))
        {
            Debug.Log("재시도");
            // 같은 씬 재로딩
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;

        }
        else if (where.Equals("goon"))
        {
            SceneManager.LoadScene("HiddenPictureScene");
        }
    }
}
