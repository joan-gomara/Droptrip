using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// afegim UI per la classe Button
using UnityEngine.UI;
// agefim TMP per el text
using TMPro;


public class GameSceneUI : MonoBehaviour
{

    public float score;
    [SerializeField] TMP_Text textScore;
    [SerializeField] Button menuButton;

     void Start()
    {
        score = 0f;
        textScore.text = "Coins: " + score.ToString();

        menuButton.onClick.AddListener(LoadMenu);
    }

    void Update()
    {
        textScore.text = "Coins: " + score.ToString();
    }

    void LoadMenu()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
