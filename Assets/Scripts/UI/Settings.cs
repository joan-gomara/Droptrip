using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] Button menuButton;
    [SerializeField] Button introButton;
    [SerializeField] Button resetButton;
    [SerializeField] Button maxButton;


    [SerializeField] SaveData _saveData;

    //[SerializeField] AudioClip audioClick;
    //private AudioSource audioSource;


    private void Start()
    {
        menuButton.onClick.AddListener(LoadMenu);
        introButton.onClick.AddListener(LoadIntro);
        resetButton.onClick.AddListener(ResetSavedata);
        maxButton.onClick.AddListener(MaxLvl);

        //audioSource = GetComponent<AudioSource>();
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    void LoadMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }

    void LoadIntro()
    {
        Loader.Load(Loader.Scene.StartAnimation);
    }

    void ResetSavedata()
    {
        _saveData = SaveManager.Load();
        _saveData._maxLevel = 0;
        _saveData._currentLevel = 0;
        SaveManager.Save(_saveData);
    }

    void MaxLvl()
    {
        _saveData = SaveManager.Load();
        _saveData._maxLevel = 4;
        _saveData._currentLevel = 3;
        SaveManager.Save(_saveData);
    }

    //void AudioClick()
    //{
    //    audioSource.PlayOneShot(audioClick, 1f);
    //}
}
