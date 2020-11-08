using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScene : MonoBehaviour
{
    public Image Leo, Leo_disable;
    public GameObject Lisa, Lisa_disable;
    public string[] AString, ALeoString, BString;
    public Text nameText, endText;
    public Image triangle;
    public Image AStart, BStart, Abg, Bbg;
    public bool[] whoTalk;

    private bool isTyping = false, isStart = true, ending = true; //HiddenPicture.totalMoney >= 1000; // true = A, false = B
    private int text_index;
    public GameObject blankbg, textUI;

    public void clickTextBox()
    {
        if (ending)
        {
            AEnding();
        }
        else
        {
            BEnding();
        }
    }
    // 진엔딩
    public void AEnding()
    {
        if (text_index == 0)
        {
            nameText.text = StartScene.charName[0];
        }
        if (isTyping)
        {
            StopAllCoroutines();
            endText.text = AString[text_index];
            isTyping = false;
        }
        else if (text_index == AString.Length - 1)
        {
            textUI.SetActive(false);
            StartCoroutine(fadeOut(Leo));
        }
        else if (!isTyping)
        {
            if (text_index == 4)
            {
                Leo.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
                Lisa.gameObject.SetActive(true);
                //StartCoroutine(displayL(Lisa));
                Leo.gameObject.SetActive(false);
                Leo_disable.gameObject.SetActive(true);
                nameText.text = StartScene.charName[1];
            }
            
            else if (!whoTalk[text_index] & text_index > 4)
            {
                nameText.text = StartScene.charName[1];
                Leo.gameObject.SetActive(false);
                Leo_disable.gameObject.SetActive(true);
                Lisa.gameObject.SetActive(true);
                Lisa_disable.gameObject.SetActive(false);
            }
            else if (whoTalk[text_index] & text_index > 4)
            {
                nameText.text = StartScene.charName[0];
                Leo.gameObject.SetActive(true);
                Leo_disable.gameObject.SetActive(false);
                Lisa.gameObject.SetActive(false);
                Lisa_disable.gameObject.SetActive(true);
            }

            StartCoroutine(_typing(++text_index, AString));
        }
        
    }

    // 일반엔딩
    public void BEnding()
    {
        nameText.text = StartScene.charName[0];
        if (isTyping)
        {
            StopAllCoroutines();
            endText.text = BString[text_index];
            isTyping = false;
        }
        else if (text_index == BString.Length - 1)
        {
            textUI.SetActive(false);
            //Leo.gameObject.SetActive(
            StartCoroutine(fadeOut(Leo));
            StartCoroutine(display(Bbg));
        }
        else
        {
            StartCoroutine(_typing(++text_index, BString));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        whoTalk = new bool[] { true, true, true, false, false, false, true, true, true, true, false, true, false, true };

        AString = new string[]
        {
            "드디어 1000냥을 다 모았어!",
            "폭신폭신한 꿈, 맛있는 음식을 잔뜩 먹는 꿈, 하늘을 나는 꿈, 온갖 좋은 꿈들을 샀어.",
            "하지만 내가 원하는 건 그런 게 아니야.",
            "내가 원하는 건, 리사의 꿈에서 마지막 인사를 전하는 거야.",
            "......",
            "레오? 레오 너야? 돌아와 준 거야? 너무 너무 보고싶었어…!",
            "레오, 나 너무 괴로웠어, 너가 나 때문에 아프고 다친 것 같아서…",
            "아니야, 내가 멋대로 리사의 곁을 떠난 것 뿐이야. 리사의 잘못이 아니야. 항상 말해 주고 싶었어.",
            "리사, 이제 악몽은 없을 거야. 내가 너의 악몽을 다 빼앗아가 버렸어. 내가 다 팔아 버렸다구.",
            "오늘부터는 폭신폭신한 꿈, 맛있는 음식을 잔뜩 먹는 꿈, 하늘을 나는 꿈, 온갖 좋은 꿈들만 꾸게 될 거야.",
            "내가 네 곁에 없어도, 너는 괜찮을 거야.",
            "레오, 나는 레오가 행복했으면 좋겠어. 어디에 있든 행복했으면 좋겠어.",
            "리사가 나에 대한 기억들로 더 이상 괴롭지 않았으면 좋겠어. 나와 함께한 시간들을 행복하게 기억해 줘.",
            "응, 그럴게, 꼭 그럴거야.",
            "좋아, 이제 떠날 수 있어.\n리사, 나는 너를 언제나 사랑해!"
        };

        BString = new string[]
        {
            "오늘도 1000냥 모으기는 실패해 버렸네!",
            "어쩔 수 없지, 나는 인내심 많은 고양이니까. 1000냥을 모을 때까지 이 아이에게 꼭 붙어 있을 거야!",
            "이 아이는 밤마다 악몽을 꾸니까, 내가 매일매일 꿈 속을 휘젓고 다녀도 모를 거야.",
            "……리사, 악몽만 꾸지 말고, 가끔은 즐거운 꿈도 꾸라고.",
            "언젠가는 내가 너의 악몽을 다 훔쳐가서, 팔아 버릴 거니까!"
        };
        if (ending)//(HiddenPicture.totalMoney > 1000)
        {
            StartCoroutine(display(AStart));
            AEnding();
        }
        else
        {
            StartCoroutine(display(BStart));
            BEnding();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void blank()
    {
        blankbg.SetActive(false);
        if (ending)
        {
            StartCoroutine(fadeOut(AStart));
        }
        else
        {
            StartCoroutine(fadeOut(BStart));
        }
        
    }
    public IEnumerator _typing(int index, string[] endString)
    {
        isTyping = true;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < endString[index].Length; i++)
        {
            endText.text = endString[index].Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        isTyping = false;
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
    IEnumerator display(Image character)
    {
        character.gameObject.SetActive(true);
        for (float i = 0.1f; i <= 1; i += 0.1f)
        {
            Color color = new Vector4(1, 1, 1, i);
            character.color = color;
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(0.001f);
        if (isStart)
        {
            blankbg.SetActive(true);
        }
    }
    IEnumerator fadeOut(Image character)
    {
        character.gameObject.SetActive(true);
        for (float i = 1; i >= 0; i -= 0.1f)
        {
            Color color = new Vector4(1, 1, 1, i);
            character.color = color;
            yield return new WaitForSeconds(0);
        }
        character.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        if (isStart)
        {
            StartCoroutine(display(Leo));
            textUI.SetActive(true);
            isStart = false;
        }
    }
}
