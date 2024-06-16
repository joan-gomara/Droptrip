using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class MainMenu : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button quitButton;


    private void Start()
    {
        playButton.onClick.AddListener(LoadScene);
        settingsButton.onClick.AddListener(LoadSettings);
        quitButton.onClick.AddListener(QuitApp);

    }

    void LoadScene()
    {
        Loader.Load(Loader.Scene.SampleScene);
    }

    void LoadSettings()
    {
        Loader.Load(Loader.Scene.Settings);

    }

    void QuitApp()
    {
        Application.Quit();
    }
}
