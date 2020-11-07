using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Audio;
using UnityEngine.UI;
public class StartScene : MonoBehaviour
{
    public Image Lisa, Lisa_disable, Leo;

    public AudioSource backMusic;
    public Slider backVolume;
    private float backVol = 1f;

    public static string[] charName = { "레오", "리사" };
    public string[] introString;
    public Text nameText, introText;
    public Image triangle, startBackground;
    public GameObject blankbg, textUI, settings_ui, startBtn;

    private int text_index;
    private bool isTyping = false;

    public void clickStart()
    {
        startBackground.gameObject.SetActive(true);
        blankbg.SetActive(true);
        startBtn.SetActive(false);
    }

    public void textAppear()
    {
        blankbg.SetActive(false);
        textUI.SetActive(true);
        StartCoroutine(display(Lisa));
        nameText.text = charName[1];
        
        StartCoroutine(_typing(text_index));
    }

    void clickTextBox()
    {
        if (text_index == 0)
        {
            nameText.text = charName[0];
            StartCoroutine(display(Leo));
            Lisa.gameObject.SetActive(false);
            Lisa_disable.gameObject.SetActive(true);
        }
        

        if (isTyping)
        {
            StopAllCoroutines();
            introText.text = introString[text_index];
            isTyping = false;
        }
        else if (text_index == introString.Length - 1)
        {
            textUI.SetActive(false);
            Lisa_disable.gameObject.SetActive(false);
            Leo.gameObject.SetActive(false);
            StartCoroutine(goToHP());
        }
        else
        {
            // 다음 대사로 넘어가기
            StartCoroutine(_typing(++text_index));
        }
    }


    public void SoundSlider()
    {
        backMusic.volume = backVolume.value;

        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);
    }

    public void clickSettings()
    {
        settings_ui.SetActive(true);
    }
    public void closeSettings()
    {
        settings_ui.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("triangle_effect");
        introString = new string[]
        { "......",
        "이 아이, 악몽을 꾸고 있어…나한테는 잘 된 일이야, 악몽을 판다면 값을 높게 부를 수 있으니까!",
        "중간에 실패만 안 하면, 1000냥은 충분히 벌 수 있다구!",
        "1000냥이면 폭신폭신한 꿈, 맛있는 음식을 잔뜩 먹는 꿈, 하늘을 나는 꿈, 온갖 좋은 꿈들을 살 수 있어.",
        "고양이는 한 번 목표로 삼은 일은 반드시 이루는 법이지",
        "오늘이야말로 이 아이의 꿈 속에서 1000냥을 벌고 말겠어!",
        "어떻게 악몽을 팔아서 돈을 벌 수 있냐고? 일단 날 따라와!"};

        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        backMusic.volume = backVolume.value;
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }
    public IEnumerator _typing(int index)
    {
        isTyping = true;
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < introString[index].Length; i++)
        {
            introText.text = introString[index].Substring(0, i);
            yield return new WaitForSeconds(0.15f);
        }
        isTyping = false;
    }
    IEnumerator goToHP()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("HiddenPictureScene");
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
    }
}
