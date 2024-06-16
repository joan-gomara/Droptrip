using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// afegim UI per la classe Button
using UnityEngine.UI;
// agefim TMP per el text
//using TMPro;


public class GameSceneUI : MonoBehaviour
{

    public float lives;
    //[SerializeField] TMP_Text textScore;



    [SerializeField] Staminabar staminabar;
    [SerializeField] Button menuButton;

     void Start()
    {
        //textScore.text = "Vides: " + lives.ToString();

        menuButton.onClick.AddListener(LoadMenu);
        staminabar.SetMaxStamina(GameManager.Instance._playerStamina.MaxStamina);
    }

    void Update()
    {
        //textScore.text = "Vides: " + lives.ToString();

        lives = GameManager.Instance._lives;
        staminabar.SetStamina(GameManager.Instance._playerStamina.Stamina);
    }

    void LoadMenu()
    {
        Loader.Load(Loader.Scene.PlayMenu);
    }
}
