using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiddenPicture : MonoBehaviour
{
    public Text tx;
    private string[] m_text = {"안녕하세요.", "반갑습니다."};

    private bool isTyping = false;

    public Text[][] choice; //[스테이지][선택지]
    public bool[][] choice_bool;

    private int text_index = 0;

    IEnumerator _typing(int index)
    {
        isTyping = true;
        yield return new WaitForSeconds(2f);
        for (int i = 0; i <= m_text[index].Length; i++)
        {
            tx.text = m_text[index].Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void clickTextBox()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            tx.text = m_text[text_index];
            isTyping = false;
        }
        else
        {
            // 다음 대사로 넘어가기
            StartCoroutine(_typing(++text_index));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        choice = new Text[3][];
        choice_bool = new bool[3][];

        StartCoroutine(_typing(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
