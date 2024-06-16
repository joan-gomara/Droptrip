using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button menuButton;

    private void Start()
    {
        newGameButton.onClick.AddListener(LoadScene);
        menuButton.onClick.AddListener(LoadMenu);
    }

    void LoadScene()
    {
        Loader.Load(Loader.Scene.SampleScene);
    }
    void LoadMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
