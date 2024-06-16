using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button menuButton;

    public bool gameFinish;

    [SerializeField] SaveData _saveData;

    private void Start()
    {
        newGameButton.onClick.AddListener(LoadScene);
        menuButton.onClick.AddListener(LoadMenu);


        _saveData = SaveManager.Load();

        print("current lvl: " + _saveData._currentLevel);
        print("max level: " + _saveData._maxLevel);

        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }

    private void Update()
    {
        if (gameFinish && _saveData._currentLevel == 4)
        {
            newGameButton.enabled = false;
        }
        else
        {
            newGameButton.enabled = true;
        }

    }

    void LoadScene()
    {
        if (_saveData._currentLevel == 0)
        {
            Loader.Load(Loader.Scene.Scene_0);
        }
        else if (_saveData._currentLevel == 1)
        {
            Loader.Load(Loader.Scene.Scene_1_1);
        }
        else if (_saveData._currentLevel == 2)
        {
            Loader.Load(Loader.Scene.Scene_1_2);
        }
        else if (_saveData._currentLevel == 3)
        {
            Loader.Load(Loader.Scene.Scene_1_3);
        }
    }
    void LoadMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }

}
