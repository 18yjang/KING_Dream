using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenPicture : MonoBehaviour
{
    // 땅을 팔지 말지
    public static int totalMoney = 0;
    public Text moneyText;
    public int stage; //1, 2, 3
    public bool[] result; // [스테이지 0/1/2] false : 반쪽 true : 완전한사진
    public Image[] resultImages, beforeImages; // <0, 1 : 스테이지 1 반, 완> <2, 3 : 스테이지 2 반, 완> <4, 5 : 스테이지 3 반, 완>
    public GameObject rippedChoice, perfectChoice, blankbg;
    public Text tx, rippedText, perfectText;
    public Image triangle, Leo;
    public bool isBefore = true;
    private string[] a_text, b_text; // = {"안녕하세요.", "반갑습니다.", "수고많으십니다."};

    private bool isTyping = true;

    private int text_index = 0;


    public IEnumerator _typing(int index, string[] m_text)
    {
       isTyping = true;
       yield return new WaitForSeconds(0.5f);
        for (int i = 0; i <= m_text[index].Length; i++)
        {
            tx.text = m_text[index].Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        isTyping = false;
    }

    public void clickTextBox()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            tx.text = a_text[text_index];
            tx.text = b_text[stage - 1];
            isTyping = false;
        }
        else if (isBefore)
        {
            blankbg.SetActive(true);
        }
        //else
        //{
        //    // 다음 대사로 넘어가기
        //    StartCoroutine(_typing(++text_index));
        //}
    }

    public void afterGround()
    {
        //stage++;
        switch (stage)
        {
            case 1:
                if (result[0]) //완전한 사진
                {
                    perfectText.text = "200냥에\n팔자!";
                    StartCoroutine(displayResult(resultImages[1]));
                    StartCoroutine(_typing(1, a_text));
                    perfectChoice.SetActive(true);
                }
                else // 반쪽
                {
                    rippedText.text = "100냥에\n팔자!";
                    StartCoroutine(displayResult(resultImages[0]));
                    StartCoroutine(_typing(0, a_text));
                    rippedChoice.SetActive(true);                    
                }
                return;
            case 2:
                if (result[1]) //완전한 사진
                {
                    perfectText.text = "500냥에\n팔자!";
                    StartCoroutine(displayResult(resultImages[3]));
                    StartCoroutine(_typing(3, a_text));
                    perfectChoice.SetActive(true);
                }
                else // 반쪽
                {
                    rippedText.text = "250냥에\n팔자!";
                    StartCoroutine(displayResult(resultImages[2]));
                    StartCoroutine(_typing(2, a_text));
                    rippedChoice.SetActive(true);
                }
                return;
            case 3:
                if (result[2]) //완전한 사진
                {
                    perfectText.text = "700냥에\n팔자!";
                    StartCoroutine(displayResult(resultImages[5]));
                    StartCoroutine(_typing(5, a_text));
                    perfectChoice.SetActive(true);
                    StartCoroutine(goToEnding());
                }
                else // 반쪽
                {
                    rippedText.text = "350냥에\n팔자!";
                    StartCoroutine(displayResult(resultImages[4]));
                    StartCoroutine(_typing(4, a_text));
                    rippedChoice.SetActive(true);
                    StartCoroutine(goToEnding());
                }
                return;
        }
    }
    public void sellClick()
    {
        rippedChoice.SetActive(false);
        perfectChoice.SetActive(false);
        switch (stage)
        {
            case 1:
                if (result[0])
                {
                    StartCoroutine(Count(200, totalMoney));
                    totalMoney += 200;
                }
                else
                {
                    StartCoroutine(Count(100, totalMoney));
                    totalMoney += 100;
                }
                return;
            case 2:
                if (result[0])
                {
                    StartCoroutine(Count(500, totalMoney));
                    totalMoney += 500;
                }
                else
                {
                    StartCoroutine(Count(250, totalMoney));
                    totalMoney += 250;
                }
                return;
            case 3:
                if (result[0])
                {
                    StartCoroutine(Count(700, totalMoney));
                    totalMoney += 700;
                }
                else
                {
                    StartCoroutine(Count(350, totalMoney));
                    totalMoney += 350;
                }
                return;
        }
    }
    public void retryClick()
    {
        rippedChoice.SetActive(false);
        switch (stage)
        {
            case 1:
                SceneManager.LoadScene("SampleScene");
                return;
            case 2:
                SceneManager.LoadScene("SampleScene");
                return;
            case 3:
                SceneManager.LoadScene("SampleScene");
                return;
        }
    }
    IEnumerator goToEnding()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndScene");
    }

    IEnumerator displayResult(Image result)
    {
        result.gameObject.SetActive(true);
        for (float i = 0.1f; i <= 1; i += 0.1f)
        {
            Color color = new Vector4(1, 1, 1, i);
            result.color = color;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(0.001f);
    }
    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = totalMoney.ToString();
        stage = 1;
        result = new bool[] { false, };

        a_text = new string[]
            {
            "이런, 기억의 절반이 없잖아!\n원래라면 200냥은 벌었을 텐데, \n이러면 100냥 밖에 벌지 못한다구.",
            "기억을 하나도 남김없이 가져왔어,\n제 값에 팔 수 있겠다!\n어서어서 1000냥을 모으자!",
            "이런, 기억의 절반이 없잖아!\n원래라면 500냥은 벌었을 텐데, 이러면 250냥 밖에 벌지 못한다구.",
            "기억을 하나도 남김없이 가져왔어,\n제 값에 팔 수 있겠다!\n어서어서 1000냥을 모으자!",
            "이런, 기억의 절반이 없잖아!\n원래라면 700냥은 벌었을 텐데, \n이러면 350냥 밖에 벌지 못한다구.",
            "기억을 하나도 남김없이 가져왔어,\n제 값에 팔 수 있겠다!\n내가 꿈에서 1000냥을 모은 고양이가 되다니, 믿기지 않아!"
            };
        b_text = new string[]
        {
            "이번 악몽은 200냥짜리야!\n고양이라면 첫번째 도전에서 실수할 리 없지!",
            "이번 악몽은 500냥짜리야!\n악몽이 점점 위험해질 거니까 각오하라구!",
            "이번 악몽은 700냥짜리야!\n가장 위험하지만, 앞에서 까먹은 돈들을 만회하고도 남는 돈이라구!"
        };
        StartCoroutine("triangle_effect");
        if (isBefore)
        {
            isTyping = true;
            switch (stage)
            {   
                case 1:
                    StartCoroutine(displayResult(beforeImages[0]));
                    StartCoroutine(displayResult(Leo));
                    StartCoroutine(_typing(0, b_text));
                    
                    return;
                case 2:
                    StartCoroutine(displayResult(beforeImages[1]));
                    StartCoroutine(displayResult(Leo));
                    StartCoroutine(_typing(1, b_text));
                    return;
                case 3:
                    StartCoroutine(displayResult(beforeImages[2]));
                    StartCoroutine(displayResult(Leo));
                    StartCoroutine(_typing(2, b_text));
                    return;
            }
        }
        else
        {
            //StartCoroutine(_typing(text_index));
            afterGround();
        }
    }
    public void clickBlank()
    {
        SceneManager.LoadScene("SampleScene");
        //beforeImages[stage - 1].gameObject.SetActive(false);
        //Leo.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator triangle_effect()
    {
        while (true)
        {
            triangle.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            triangle.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator Count(float target, float current)

    {

        float duration = 0.5f; // 카운팅에 걸리는 시간 설정. 

        float offset = (target - current) / duration; // 



        while (current < target)

        {
            current += offset * Time.deltaTime;
            moneyText.text = ((int)current).ToString();
            yield return null;

        }
        current = target;
        moneyText.text = ((int)current).ToString();
    }
}
