using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBehaviour : MonoBehaviour
{

    PlayerController _playerController;
    private bool faceRight = true;
    private float _move;
    


    [SerializeField] SaveData _saveData;


    private void Start()
    {
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();


    }

    void Update()
    {
        _move = _playerController.moveHorizontal;

        // gira el Sprite en funció de la direcció
        if (_move > 0 && !faceRight)
        {
            faceRight = !faceRight;
        }
        else if (_move < 0 && faceRight)
        {
            faceRight = !faceRight;
        }


        //quit game
        if (Input.GetKeyDown("escape"))
        {
            Loader.Load(Loader.Scene.MainMenu);
        }

    }


    // Aquesta actualització s'executa després que tots els processos hagin sigut executats
    // D'aquesta manera ens assegurem que la inversió del personatge és realitza despés del "Animate"
    private void LateUpdate()
    {
        if (!faceRight)
        {
            Vector3 flipScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = flipScale;
            //GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    /*
    A typical workaround is give the game object an empty parent and change its scale.

    Player    <---- Change scale here
        Model <---- Animator here
     */


    // Savedata Manager:

    public void SavePos()
    {
        //_saveData._currentLevel = SceneManager.GetActiveScene().buildIndex;
        //_saveData._playerHealth = 100;
        Debug.Log("Saving game ...");

        _saveData = SaveManager.Load();

        //podem accedir directament a les propietats de la classe _saveData perque està serialitzada
        _saveData._playerLocationX = gameObject.transform.position.x;
        _saveData._playerLocationY = gameObject.transform.position.y;
        _saveData._playerLocationZ = gameObject.transform.position.z;
        //_saveData._playerStamina = GameManager.Instance._playerStamina.Stamina;
        _saveData._playerStamina = GameManager.Instance._playerStamina.MaxStamina;
        _saveData._currentLevel = GameManager.Instance.level;

        SaveManager.Save(_saveData);
    }

    public void LoadPos()
    {
        //Debug.Log("Loading game ...");
        _saveData = SaveManager.Load();

        gameObject.transform.position = new Vector3(_saveData._playerLocationX,
            _saveData._playerLocationY, _saveData._playerLocationZ);
        GameManager.Instance._playerStamina.Stamina = _saveData._playerStamina;
    }

    public void ResetPos()
    {
        //Debug.Log("Restart game");

        _saveData = SaveManager.Load();

        _saveData._playerLocationX = 0f;
        _saveData._playerLocationY = 0f;
        _saveData._playerLocationZ = 0f;
        _saveData._playerStamina = GameManager.Instance._playerStamina.MaxStamina;
        _saveData._currentLevel = GameManager.Instance.level;
     
        SaveManager.Save(_saveData);
    }


    public void UpLevel()
    {
        _saveData = SaveManager.Load();
        if (GameManager.Instance.level < 4)
        {
            _saveData._currentLevel = GameManager.Instance.level + 1;
            SaveManager.Save(_saveData);

            print("current lvl ++: " + _saveData._currentLevel);
        }

        if (_saveData._currentLevel > _saveData._maxLevel)
        { 
            _saveData = SaveManager.Load();
            _saveData._maxLevel = _saveData._currentLevel;
            SaveManager.Save(_saveData);

            print("max level ++: " + _saveData._maxLevel);
        }

        
    }

}
