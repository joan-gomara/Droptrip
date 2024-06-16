using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayMenu : MonoBehaviour
{
    [SerializeField] Button tutorialButton;
    [SerializeField] Button lvl1Button;
    [SerializeField] Button lvl2Button;
    [SerializeField] Button lvl3Button;

    [SerializeField] Button backButton;

    [SerializeField] SaveData _saveData;

    private void Start()
    {
        tutorialButton.onClick.AddListener(LoadTutorial);
        lvl1Button.onClick.AddListener(Load1lvl);
        lvl2Button.onClick.AddListener(Load2lvl);
        lvl3Button.onClick.AddListener(Load3lvl);
        backButton.onClick.AddListener(LoadMainMenu);

        tutorialButton.enabled = true;

        lvl1Button.enabled = false;
        lvl2Button.enabled = false;
        lvl3Button.enabled = false;

        if(!SaveManager.SavedataExists())
        {
            print("create new savedata, lvl 0");
            _saveData._maxLevel = 0;
            SaveManager.Save(_saveData);
        }

        _saveData = SaveManager.Load();

        print("current: "+ _saveData._currentLevel);
        print("max: "+ _saveData._maxLevel);

        Screen.orientation = ScreenOrientation.LandscapeLeft;


    }


    private void Update()
    {
        int level = _saveData._maxLevel;
        
        if (level > 0)
        {
            lvl1Button.enabled = true;
        }
        if (level > 1)
        {
            lvl2Button.enabled = true;
        }
        if (level > 2)
        {
            lvl3Button.enabled = true;
        }


    }


    void LoadTutorial()
    {
        Loader.Load(Loader.Scene.Scene_0);
    }

    void Load1lvl()
    {
        Loader.Load(Loader.Scene.Scene_1_1);

    }

    void Load2lvl()
    {
        Loader.Load(Loader.Scene.Scene_1_2);
    }

    void Load3lvl()
    {
        Loader.Load(Loader.Scene.Scene_1_3);
    }

    void LoadMainMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }

}
