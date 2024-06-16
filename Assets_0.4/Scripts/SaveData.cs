using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//serialitzation allows for us to store objects into memory, databases or files
[System.Serializable]
// we cannot use static or private 

public class SaveData
{
    public int _currentLevel;
    public int _playerHealth;
    public int _playerExp;
    public float _playerLocationX;
    public float _playerLocationY;
    public float _playerLocationZ;
    // C:\Users\joan\AppData\LocalFlow\DefaulCompany\...
}
