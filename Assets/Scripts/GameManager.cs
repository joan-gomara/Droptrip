using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // es pot accedir directament als objectes d'aquesta claase des de qualsevol script 
    public static GameManager Instance{ get; private set; }


    public int level;
    public int _lives;
    public int _maxLives = 3;


    
    public static float _maxStamina = 100f;
    public static float _staminaWaste = 1.5f;

    // Es crea l'objecte i la stamina del jugador amb la classe creada anteriorment
    // d'aquesta manera podem crear varis jugadors o enemics amb una stamina determinada
    public UnitStamina _playerStamina = new UnitStamina(_maxStamina, _maxStamina, _staminaWaste, false);



    void Awake()
    {
        if (true)
        {
            
            if (Instance != null && Instance != this)
            {
                //if there are duplicate destroy it
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }  
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

}
