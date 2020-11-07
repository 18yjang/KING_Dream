using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Audio;
using UnityEngine.UI;
public class StartScene : MonoBehaviour
{
    public AudioSource backMusic;
    public Slider backVolume;
    private float backVol = 1f;

    public GameObject settings_ui;

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
    // Start is called before the first frame update
    void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        backMusic.volume = backVolume.value;
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }
}
