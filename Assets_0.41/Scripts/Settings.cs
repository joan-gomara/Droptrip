using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] Button menuButton;

    private void Start()
    {
        menuButton.onClick.AddListener(LoadMenu);
    }

    void LoadMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
