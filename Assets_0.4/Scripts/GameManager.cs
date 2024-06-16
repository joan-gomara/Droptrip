using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // es pot accedir directament als objectes d'aquesta claase des de qualsevol script 
    public static GameManager Instance{ get; private set; }


    // Es crea els objectes de la vida i la stamina del jugador amb les classes creades anteriorment
    public UnitHealth _playerHealth = new UnitHealth(100, 100);
    public UnitStamina _playerStamina = new UnitStamina(100f, 100f, 30f, false);

    public int _level = 0;


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

}
